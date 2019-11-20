using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections; //192.168.43.123

namespace ClientAlfaCSharp
{
    class Program
    {
        public static BitArray bitArraySession11 = new BitArray(11, false);

        static void Main(string[] args)
        {
            int[] sesja = new int[1];
            int licznik = 0;
            int bitSendLenght = 144;                                                                        //ilość wysyłanych bitów
            string IPserver = "127.0.0.1";                                                                  //IP odbiorcy - servera
            int port = 8080;                                                                                //Port
            IPEndPoint sendingEndPoint = new IPEndPoint(IPAddress.Parse(IPserver), port);                   //Miejsce gdzie będzie wysyłane
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);     //Określenie protokołów z których korzysta klient
            string action;                                                                                  //Pole okreslające akcje użytkownika
            byte[] dataSend = new byte[bitSendLenght / 8];                                                  //tablica która będziemy wysyłać dane
            byte[] historyRequest80 = new byte[10];                                                           //tablica wykorzystywana do wysłania żądania odnośnie historii
            byte[] historyRequest112 = new byte[14];                                                           //tablica wykorzystywana do wysłania żądania odnośnie historii

            Console.WriteLine("Witaj podaj, co chcesz zrobić:\n d -dodawanie\n o -odejmowanie\n m -mnożenie\n dz -dzielenie\n s -silnia\n hs -historia z użyciem sejsi\n hi -historia z użyciem ID obliczen\n  hsp -historia z podanym ID sesji\n hip -historia z podanym ID obliczeń\n cls -wyczyść konsole\n");


            while (true)
            {
                if (licznik == 1)
                {
                    CoddingClass.SessionToIntArray(ref sesja, bitArraySession11);
                    Console.WriteLine("\nUzyskano sesje: " + sesja[0]);
                }

                action = Console.ReadLine();
                if (action == "cls")
                {
                    licznik++;
                    Console.Clear();
                    Console.WriteLine("Numer sesji: " + sesja[0]);
                    Console.WriteLine("Witaj podaj, co chcesz zrobić:\n d -dodawanie\n o -odejmowanie\n m -mnożenie\n dz -dzielenie\n s -silnia\n hs -historia z użyciem sejsi\n hi -historia z użyciem ID obliczen\n hsp -historia z podanym ID sesji\n hip -historia z podanym ID obliczeń\n cls -wyczyść konsole\n");
                    continue;
                }
                else if (action == "d" || action == "m" || action == "o" || action == "dz" /*|| action == "s" || action == "h"*/)
                    CalculationClass.Calculation(action, ref dataSend, ref bitArraySession11);
                else if (action == "s")
                {
                    CalculationClass.factorial(action, ref dataSend, ref bitArraySession11);
                }
                else if (action == "hs" || action == "hi" || action =="hip" || action =="hsp")
                {

                    if(action == "hs" || action == "hi")
                        historyRequest80 = HistorClass.HistoryRequest(action, bitArraySession11);
                    else
                        historyRequest112 = HistorClass.HistoryRequest(action, bitArraySession11);
                }
                else
                {
                    Console.WriteLine("Podana operacja nie istnieje!");
                    continue;
                }

                if (action == "hs" || action == "hi")
                {
                    client.SendTo(historyRequest80, sendingEndPoint);
                }
                else if(action == "hip" || action =="hsp")
                {
                    client.SendTo(historyRequest112, sendingEndPoint);
                }
                else
                {
                    client.SendTo(dataSend, sendingEndPoint);
                }

                ListenClass.StartListener(ref bitArraySession11);
                licznik++;

            }
            Console.ReadLine();
        }
    }
}
