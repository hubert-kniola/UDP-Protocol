using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace ServerAlfaCSharp
{
    class FeedbackMessage
    {
        public static HistoryClass[] history = new HistoryClass[100];
        public static int history_count = 0;


        static int[] temp = new int[1];
        public static void feedback(int result_calculate, BitArray recvBitArray /*odebrany bitArray*/)
        {
            history[history_count] = new HistoryClass();                                        //Wywołanie konstruktora
            BitArray SendBitArrayToTheClinet = new BitArray(112, false);                        //Tworzenie Tablicy wysyłanej do klineta
            temp[0] = result_calculate;
            Console.WriteLine("Wynik operacji: " + temp[0]);
            BitArray resultBitArray = new BitArray(temp);                                       //Tworzenie BItArray z wynikiem działania
            resultBitArray = ReverseClass.reverseOrder(resultBitArray);                         //Odwrócenie kolejności
            for (int i = 2; i < 5; i++)                                                         //Przekopiowanie POLA STATUSU
                SendBitArrayToTheClinet[i] = recvBitArray[i];
            for (int i = 0; i < 62; i++)
                SendBitArrayToTheClinet[i] = recvBitArray[i];
            for (int i = 63; i < 69; i++)                                                       //Kodowanie pola długosci bitow
                if (i == 63 || i == 65 || i == 67 || i == 68)                                   //Liczba 43
                    SendBitArrayToTheClinet[i] = true;
            for (int i = 69, j = 0; i < 101 && j < SendBitArrayToTheClinet.Count; i++, j++)     //Kodowanie wyniku
                SendBitArrayToTheClinet[i] = resultBitArray[j];
            for (int i = 133, j = 101; i < 144 && j < 112; i++, j++)                            //Przypisanie sesji do tablicy wysyłanej do klineta
            {
                SendBitArrayToTheClinet[j] = recvBitArray[i];
                //Console.Write(SendBitArrayToTheClinet[j] + " ");
            }
            Console.WriteLine();

            history[history_count] = HistoryClass.SaveStories(recvBitArray, SendBitArrayToTheClinet);
            history_count++;                                                                    //licznik pozycji historii

            byte[] sendByteToTheClient = new byte[SendBitArrayToTheClinet.Count / 8];           //Stworzenie tablicy byte to wysylki
            sendByteToTheClient = CoddingClass.CoddingBitToByteArray(SendBitArrayToTheClinet);
            SendClass.SendMessageToTheClient(sendByteToTheClient);                              //Wysłanie wiadomośći do klineta
            Console.WriteLine("Wiadomosć została wysłana");
        }

        public static void SendHistory(BitArray recvBit /*MA WARTOŚĆ 80*/)
        {
            int check = 0;
            SessionClass.session(recvBit);

            for (int i = 0, j = recvBit.Count - 11; i < SessionClass.SessionCheck.Count && j < recvBit.Count; i++, j++)
                if (SessionClass.SessionCheck[i] == recvBit[j])
                    check++;

            if (history_count == 0/* || check>0*/)
            {
                BitArray sendHistoryToTheClient = new BitArray(80, false);
                sendHistoryToTheClient[2] = sendHistoryToTheClient[3] = true;                    //Bład historii - historia pusta
                sendHistoryToTheClient[65] = sendHistoryToTheClient[67] = sendHistoryToTheClient[68] = true;
                for (int i = 69; i < sendHistoryToTheClient.Count; i++)
                    sendHistoryToTheClient[i] = recvBit[i];

                //WriteClass.WriteBitTab(sendHistoryToTheClient);
                byte[] sendByteToTheClient = new byte[sendHistoryToTheClient.Count / 8];
                sendByteToTheClient = CoddingClass.CoddingBitToByteArray(sendHistoryToTheClient);
                SendClass.SendMessageToTheClient(sendByteToTheClient);                          //Wysyła tablice 80 bitów 
            }
            else if (history_count > 0)  //historia z użyciem id sesji
            {
                int sizeOfData = 0;
                int howMuch = 0;
                int test = 0;

                if (recvBit[0] == false && recvBit[1] == false && recvBit[2] == false && recvBit[3] == true && recvBit[4] == true)              //Historia z użyciem id Sesji
                {
                    for (int i = 0; i < history_count; i++)                                        //Sprawzamy ile elementów odpowiada warunkowi
                    {
                        for (int j = 0, k = 69; j < history[i].session.Count && k < recvBit.Count; j++, k++)
                            if (history[i].session[j] == recvBit[k])
                                test++;
                    }
                    howMuch = test / 11;

                    HistoryClass[] historyToSend = new HistoryClass[howMuch];

                    for (int i = 0, l = 0; i < history_count && l < howMuch; i++)
                    {
                        test = 0;
                        for (int j = 0, k = 69; j < history[i].session.Count && k < recvBit.Count; j++, k++)
                            if (history[i].session[j] == recvBit[k])
                                test++;
                        if (test == 11)
                        {
                            historyToSend[l] = new HistoryClass();
                            for (int a = 0; a < historyToSend[l].operation_ID.Count; a++)             //Kopia ID operacji
                                historyToSend[l].operation_ID[a] = history[i].operation_ID[a];
                            for (int a = 0; a < historyToSend[l].variable_1.Count; a++)               //Kopia zmiennej 1
                                historyToSend[l].variable_1[a] = history[i].variable_1[a];
                            for (int a = 0; a < historyToSend[l].variable_2.Count; a++)               //Kopia zmiennej 2
                                historyToSend[l].variable_2[a] = history[i].variable_2[a];
                            for (int a = 0; a < historyToSend[l].result.Count; a++)                   //Kopia wyniku
                                historyToSend[l].result[a] = history[i].result[a];
                            for (int a = 0; a < historyToSend[l].session.Count; a++)                  //Kopia sesji
                                historyToSend[l].session[a] = history[i].session[a];
                            l++;                                                                      //inkrementacja pozycji w tablicy do wysłania
                        }
                    }

                    //Zakodowanie wiadomości do klienta
                    BitArray sendSizeHistoryToTheClient = new BitArray(112, false);                  //Tablica posiadająca rozmiar danych
                    temp[0] = howMuch;
                    BitArray numbersOfStories = new BitArray(temp);                                       //Tworzenie BItArray z wynikiem działania
                    numbersOfStories = ReverseClass.reverseOrder(numbersOfStories);
                    sendSizeHistoryToTheClient[2] = sendSizeHistoryToTheClient[4] = true;            //Zakodowanie ilości plików w historii
                    //Kodowanie długości bitów
                    sendSizeHistoryToTheClient[63] = sendSizeHistoryToTheClient[65] = sendSizeHistoryToTheClient[67] = sendSizeHistoryToTheClient[68] = true;
                    for (int i = 69, j = 0; i < sendSizeHistoryToTheClient.Count - 11 && j < numbersOfStories.Count; i++, j++)
                        sendSizeHistoryToTheClient[i] = numbersOfStories[j];
                    //Kodowanie sesji
                    for (int i = 101, j = 69; i < sendSizeHistoryToTheClient.Count && j < recvBit.Count; i++, j++)
                        sendSizeHistoryToTheClient[i] = recvBit[j];

                    byte[] sendByteToTheClient = new byte[sendSizeHistoryToTheClient.Count / 8];           //Stworzenie tablicy byte to wysylki
                    sendByteToTheClient = CoddingClass.CoddingBitToByteArray(sendSizeHistoryToTheClient);
                    SendClass.SendMessageToTheClient(sendByteToTheClient);



                    BitArray sendHistoryFileToTheClient = new BitArray(176, false);                  //Wysyłane dane
                                                                                                     //Kodowanie długości bitów
                    sendHistoryFileToTheClient[62] = sendHistoryFileToTheClient[63] = sendHistoryFileToTheClient[65] = sendHistoryFileToTheClient[67] = sendHistoryFileToTheClient[68] = true;
                    //Wysyłanie wiadomości do klineta
                    byte[] FinishHistryToSend = new byte[176 / 8];
                    for (int a = 0; a < howMuch; a++)
                    {
                        for (int i = 0, j = 0; i < 2 && j < historyToSend[a].operation_ID.Count; i++, j++)      //ID OPERACJI
                            sendHistoryFileToTheClient[i] = historyToSend[a].operation_ID[j];
                        for (int i = 69, j = 0; i < 101 && j < historyToSend[a].variable_1.Count; i++, j++)     //VAL 1
                            sendHistoryFileToTheClient[i] = historyToSend[a].variable_1[j];
                        for (int i = 101, j = 0; i < 133 && j < historyToSend[a].variable_2.Count; i++, j++)     //VAL 2
                            sendHistoryFileToTheClient[i] = historyToSend[a].variable_2[j];
                        for (int i = 133, j = 0; i < 165 && j < historyToSend[a].result.Count; i++, j++)     //result
                            sendHistoryFileToTheClient[i] = historyToSend[a].result[j];
                        for (int i = 165, j = 0; i < 176 && j < historyToSend[a].session.Count; i++, j++)     //sesja
                            sendHistoryFileToTheClient[i] = historyToSend[a].session[j];

                        sendHistoryFileToTheClient[3] = sendHistoryFileToTheClient[4] = true;

                        FinishHistryToSend = CoddingClass.CoddingBitToByteArray(sendHistoryFileToTheClient);
                        SendClass.SendMessageToTheClient(FinishHistryToSend);
                    }



                }
                else if (recvBit[2] == true && recvBit[3] == false && recvBit[4] == false)       //historia z użyciem id obliczen
                {
                    for (int i = 0; i < history_count; i++)                                        //Sprawzamy ile elementów odpowiada warunkowi
                    {
                        for (int j = 0, k = 69; j < history[i].session.Count && k < recvBit.Count; j++, k++)
                        {
                            if (history[i].session[j] == recvBit[k])
                                if (history[i].operation_ID[0] == recvBit[0] && history[i].operation_ID[1] == recvBit[1])
                                    if (recvBit[0] == false && recvBit[1] == false)
                                    {
                                        if (checkMultiply(history[i]) == true)
                                            test++;
                                    }
                       
                                    else
                                        test++;


                            //if (recvBit[0] == false && recvBit[1] == false)
                            //    if (checkMultiply(history[i]) == false)
                            //        continue;
                        }
                    }
                    howMuch = test / 11;

                    HistoryClass[] historyToSend = new HistoryClass[howMuch];

                    for (int i = 0, l = 0; i < history_count && l < howMuch; i++)
                    {
                        test = 0;
                        for (int j = 0, k = 69; j < history[i].session.Count && k < recvBit.Count; j++, k++)
                            if (history[i].session[j] == recvBit[k])
                                if (history[i].operation_ID[0] == recvBit[0] && history[i].operation_ID[1] == recvBit[1])
                                    test++;

                        if (test == 11)
                        {
                            if (recvBit[0] == false && recvBit[1] == false)
                                if (checkMultiply(history[i]) == false)
                                    continue;

                            historyToSend[l] = new HistoryClass();
                            for (int a = 0; a < historyToSend[l].operation_ID.Count; a++)             //Kopia ID operacji
                                historyToSend[l].operation_ID[a] = history[i].operation_ID[a];
                            for (int a = 0; a < historyToSend[l].variable_1.Count; a++)               //Kopia zmiennej 1
                                historyToSend[l].variable_1[a] = history[i].variable_1[a];
                            for (int a = 0; a < historyToSend[l].variable_2.Count; a++)               //Kopia zmiennej 2
                                historyToSend[l].variable_2[a] = history[i].variable_2[a];
                            for (int a = 0; a < historyToSend[l].result.Count; a++)                   //Kopia wyniku
                                historyToSend[l].result[a] = history[i].result[a];
                            for (int a = 0; a < historyToSend[l].session.Count; a++)                  //Kopia sesji
                                historyToSend[l].session[a] = history[i].session[a];
                            l++;                                                                      //inkrementacja pozycji w tablicy do wysłania
                        }
                    }

                    //Zakodowanie wiadomości do klienta
                    BitArray sendSizeHistoryToTheClient = new BitArray(112, false);                  //Tablica posiadająca rozmiar danych
                    temp[0] = howMuch;
                    BitArray numbersOfStories = new BitArray(temp);                                       //Tworzenie BItArray z wynikiem działania
                    numbersOfStories = ReverseClass.reverseOrder(numbersOfStories);
                    sendSizeHistoryToTheClient[2] = sendSizeHistoryToTheClient[4] = true;            //Zakodowanie ilości plików w historii
                    sendSizeHistoryToTheClient[63] = sendSizeHistoryToTheClient[65] = sendSizeHistoryToTheClient[67] = sendSizeHistoryToTheClient[68] = true;            //Zakodowanie długości bitów


                    for (int i = 69, j = 0; i < sendSizeHistoryToTheClient.Count - 11 && j < numbersOfStories.Count; i++, j++)
                        sendSizeHistoryToTheClient[i] = numbersOfStories[j];
                    //Kodowanie sesji
                    for (int i = 101, j = 69; i < sendSizeHistoryToTheClient.Count && j < recvBit.Count; i++, j++)
                        sendSizeHistoryToTheClient[i] = recvBit[j];

                    byte[] sendByteToTheClient = new byte[sendSizeHistoryToTheClient.Count / 8];           //Stworzenie tablicy byte to wysylki
                    sendByteToTheClient = CoddingClass.CoddingBitToByteArray(sendSizeHistoryToTheClient);
                    SendClass.SendMessageToTheClient(sendByteToTheClient);

                    BitArray sendHistoryFileToTheClient = new BitArray(176, false);                  //Wysyłane dane

                    //Wysyłanie wiadomości do klineta
                    byte[] FinishHistryToSend = new byte[176 / 8];
                    //Kodowanie długości bitów
                    sendHistoryFileToTheClient[62] = sendHistoryFileToTheClient[63] = sendHistoryFileToTheClient[65] = sendHistoryFileToTheClient[67] = sendHistoryFileToTheClient[68] = true;

                    for (int a = 0; a < howMuch; a++)
                    {
                        for (int i = 0, j = 0; i < 2 && j < historyToSend[a].operation_ID.Count; i++, j++)      //ID OPERACJI
                            sendHistoryFileToTheClient[i] = historyToSend[a].operation_ID[j];
                        for (int i = 69, j = 0; i < 101 && j < historyToSend[a].variable_1.Count; i++, j++)     //VAL 1
                            sendHistoryFileToTheClient[i] = historyToSend[a].variable_1[j];
                        for (int i = 101, j = 0; i < 133 && j < historyToSend[a].variable_2.Count; i++, j++)     //VAL 2
                            sendHistoryFileToTheClient[i] = historyToSend[a].variable_2[j];
                        for (int i = 133, j = 0; i < 165 && j < historyToSend[a].result.Count; i++, j++)     //result
                            sendHistoryFileToTheClient[i] = historyToSend[a].result[j];
                        for (int i = 165, j = 0; i < 176 && j < historyToSend[a].session.Count; i++, j++)     //sesja
                            sendHistoryFileToTheClient[i] = historyToSend[a].session[j];

                        sendHistoryFileToTheClient[2] = true;
                        sendHistoryFileToTheClient[3] = false;
                        sendHistoryFileToTheClient[4] = false;

                        FinishHistryToSend = CoddingClass.CoddingBitToByteArray(sendHistoryFileToTheClient);
                        SendClass.SendMessageToTheClient(FinishHistryToSend);
                    }

                }
            }
        }

        static bool checkMultiply(HistoryClass check_history_file)
        {
            int val_1 = getValue(check_history_file.variable_1);
            int val_2 = getValue(check_history_file.variable_2);
            int result = getValue(check_history_file.result);

            if (val_1 * val_2 != result)
                return false;
            else
                return true;
        }

        static int getValue(BitArray ValueToCheck)
        {
            double result = 0;
            double pow = ValueToCheck.Count - 1;
            for (int i = 0; i < ValueToCheck.Count; i++)
            {
                if (ValueToCheck[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }
            return (int)result;

        }
    }
}
