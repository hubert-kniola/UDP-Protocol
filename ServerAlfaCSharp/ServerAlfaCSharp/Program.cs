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
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[144 / 8];
            int[] val_1 = new int[1];                                                                       //W tej tablicy będzie przechowywana wartosc 1
            int[] val_2 = new int[1];                                                                       //W tej tablicy będzie przechowywana wartosc 2
            int serverPort = 8080;                                                                          //ustawienie portu
            IPEndPoint listeningPort = new IPEndPoint(IPAddress.Any, serverPort);                           //ustawienie miejsca nasłuchiwania
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);     //określenie protokołów
            server.Bind(listeningPort);                                                                     //Bindowanie servera na danym porcie

            Console.WriteLine("Waiting for a client...");
            //EndPoint tmpRemote = listeningPort;
            //server.ReceiveFrom(data, ref tmpRemote);


            while (true)
            {
                EndPoint tmpRemote = listeningPort;
                server.ReceiveFrom(data, ref tmpRemote);                                                             //odbieranie danych
                BitArray recevBit = DecoddingClass.decoddingByteToBitArray(data);                                    //konwersja z byte to bitArray
                if (recevBit[2] == false && recevBit[3] == false && (recevBit[4] == false || recevBit[4] == true))   //działania + - * /
                {
                    CoddingClass.CoddingBitToIntArray_1(ref val_1, recevBit);                                        //zamiana Bit na Int
                    CoddingClass.CoddingBitToIntArray_2(ref val_2, recevBit);                                        //zamiana Bit na Int

                    CalculationsClas.Calculation(val_1, val_2, recevBit);
                }
                else if (recevBit[2] == false && recevBit[3] == true && recevBit[4] == true)                        //historia według sesji
                {
                    HistoryClass.TransformBit144toBit80Array(recevBit);

                }   
                else if (recevBit[2] == true && recevBit[3] == false && recevBit[4] == false)                        //historia według obliczen
                {
                    HistoryClass.TransformBit144toBit80Array(recevBit);
                }
                else if(recevBit[2] == true && recevBit[3] == false && recevBit[4] == true)                          //HSP
                {
                    
                    HistoryClass.TransformBit144toBit112Array(recevBit);
                }
                else if(recevBit[2] == true && recevBit[3] == true && recevBit[4] == false)                          //HIP
                {
                    HistoryClass.TransformBit144toBit112Array(recevBit);
                }

                        //CalculationsClas.Calculation(val_1, val_2, recevBit);

            }


            Console.ReadLine();
        }

    }
}
