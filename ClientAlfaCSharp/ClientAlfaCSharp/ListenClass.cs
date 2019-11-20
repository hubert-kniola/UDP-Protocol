using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace ClientAlfaCSharp
{
    class ListenClass
    {
        private const int listenPort = 8081;
        public static int[] result = new int[1];
        public static int[] historyInt = new int[3];
        public static int check;

        public static void StartListener(ref BitArray bitArraySession11)
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            int negative_number_counter = 0;

            try
            {
            poczatek:

                byte[] bytes = listener.Receive(ref groupEP);


               
                BitArray recvBitArray = DecoddingClass.decoddingByteToBitArray(bytes);
                if (recvBitArray[2] == false && recvBitArray[3] == false && (recvBitArray[4] == false || recvBitArray[4] == true))
                {
                    CoddingClass.CoddingBitToIntArray(ref result, recvBitArray);
                    Console.WriteLine("Wynik: " + result[0]);
                }
                else if (recvBitArray[2] == false && recvBitArray[3] == true && recvBitArray[4] == false)
                {
                    CoddingClass.CoddingBitToIntArray(ref result, recvBitArray);
                    negative_number_counter++;
                    if (negative_number_counter > 0)
                        Console.WriteLine("Wynik: -" + result[0]);
                    else
                        Console.WriteLine(result[0]);
                }
                else if (recvBitArray[2] == true && recvBitArray[3] == true && recvBitArray[4] == false && recvBitArray.Count == 80 )
                {//Błąd histori
                    Console.WriteLine("Historia jest pusta!");
                }
                else if (recvBitArray[2] == true && recvBitArray[3] == false && recvBitArray[4] == true && recvBitArray.Count == 112) //Odbiór historii z użyciem sesji
                {//ilość plików historii
                    CoddingClass.CoddingBitToIntArray(ref result, recvBitArray);
                    check = result[0];
                    goto poczatek;
                }
                else if (recvBitArray[2] == false && recvBitArray[3] == true && recvBitArray[4] == true && recvBitArray.Count == 176)
                {//odbieranie historii

                    historyInt = CoddingClass.CoddingBitToIntArrayHistory(recvBitArray);
                    if (recvBitArray[0] == false && recvBitArray[1] == false)
                    {
                        if (historyInt[0] * historyInt[1] != historyInt[2])
                            Console.WriteLine(historyInt[0] + " ! " + " = " + historyInt[2]);
                        else
                            Console.WriteLine(historyInt[0] + " * " + historyInt[1] + " = " + historyInt[2]);
                    }
                    else if (recvBitArray[0] == false && recvBitArray[1] == true)
                        Console.WriteLine(historyInt[0] + " : " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == true && recvBitArray[1] == false)
                        Console.WriteLine(historyInt[0] + " + " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == true && recvBitArray[1] == true)
                        Console.WriteLine(historyInt[0] + " - " + historyInt[1] + " = " + historyInt[2]);
                    check--;
                    if (check > 0)
                        goto poczatek;
                }
                else if (recvBitArray[2] == true && recvBitArray[3] == false && recvBitArray[4] == true && recvBitArray.Count == 176)
                {//odbieranie historii
                    
                    historyInt = CoddingClass.CoddingBitToIntArrayHistory(recvBitArray);
                    if (recvBitArray[0] == false && recvBitArray[1] == false)
                    {
                        if(historyInt[0]*historyInt[1]!=historyInt[2])
                            Console.WriteLine(historyInt[0] + " ! " + " = " + historyInt[2]);
                        else
                            Console.WriteLine(historyInt[0] + " * " + historyInt[1] + " = " + historyInt[2]);
                    }
                    else if (recvBitArray[0] == false && recvBitArray[1] == true)
                        Console.WriteLine(historyInt[0] + " : " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == true && recvBitArray[1] == false)
                        Console.WriteLine(historyInt[0] + " + " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == true && recvBitArray[1] == true)
                        Console.WriteLine(historyInt[0] + " - " + historyInt[1] + " = " + historyInt[2]);
                    check--;
                    if (check > 0)
                        goto poczatek;
                }
                else if (recvBitArray[2] == true && recvBitArray[3] == false && recvBitArray[4] == false && recvBitArray.Count == 112) //Odbiór historii z użyciem sesji
                {//ilość plików historii
                    CoddingClass.CoddingBitToIntArray(ref result, recvBitArray);
                    check = result[0];
                    goto poczatek;
                }
                else if (recvBitArray[2] == true && recvBitArray[3] == false && recvBitArray[4] == false && recvBitArray.Count == 176)
                {//odbieranie historii

                    historyInt = CoddingClass.CoddingBitToIntArrayHistory(recvBitArray);
                    if (recvBitArray[0] ==  false && recvBitArray[1] == false)
                        if (historyInt[0] * historyInt[1] != historyInt[2])
                            Console.WriteLine(historyInt[0] + " ! " + " = " + historyInt[2]);
                        else
                            Console.WriteLine(historyInt[0] + " * " + historyInt[1] + " = " + historyInt[2]);
                    //Console.WriteLine(historyInt[0] + " * " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == false && recvBitArray [1] == true)
                        Console.WriteLine(historyInt[0] + " : " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] == true && recvBitArray[1] == false)
                        Console.WriteLine(historyInt[0] + " + " + historyInt[1] + " = " + historyInt[2]);
                    else if (recvBitArray[0] ==  true && recvBitArray[1] == true)
                        Console.WriteLine(historyInt[0] + " - " + historyInt[1] + " = " + historyInt[2]);
                    check--;
                    if (check > 0)
                        goto poczatek;
                }
                else if(recvBitArray[0] == false && recvBitArray[1] ==true && recvBitArray[2] == true && recvBitArray[3] == true && recvBitArray[4] == true && recvBitArray.Count ==80)
                    Console.WriteLine("Błąd dzielenia przez zero!");
                else if (recvBitArray[2] == true && recvBitArray[3] == true && recvBitArray[4] == true)
                    Console.WriteLine("Wynik znajduje się poza zakresem  <0,{0}>", int.MaxValue);
                CoddingClass.CoddingSession(ref bitArraySession11, recvBitArray);

            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
