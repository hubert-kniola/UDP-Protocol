using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{
    class SessionClass
    {
        static public BitArray SessionCheck = new BitArray(11,false);
        static int randomInt()
        {
            Random randomValue = new Random();
            int rV = randomValue.Next(0, 2047);                     //Operujemy na 11 bitach, zatem max liczba do osiągnięcia to 2^11 - 1
            return rV;
        }

        public static void session(BitArray bitArray)
        {
            
            double session_result = 0;
            double pow = 10;
            int session_draw = 0;                                   //Losowanie sesji 
            if (bitArray.Count == 144)
            {
                for (int i = 133; i < 144; i++)                      //Obliczamy sesje
                {
                    if (bitArray[i] == true)
                        session_result += Math.Pow(2, pow);
                    pow--;
                }
            }
            else if (bitArray.Count == 112)
            {
               
                for (int i = 101; i < 112; i++)                      //Obliczamy sesje
                {
                    if (bitArray[i] == true)
                        session_result += Math.Pow(2, pow);
                    pow--;
                }
            }
            else if (bitArray.Count == 80)
            {
                for (int i = 69; i < 80; i++)                      //Obliczamy sesje
                {
                    if (bitArray[i] == true)
                        session_result += Math.Pow(2, pow);
                    pow--;
                }
            }


            if (session_result == 0 && bitArray.Count == 144)       //Ten if wystartuje tylko wtedy, gdy nasza sesja wynosi 0 - czyli tylko przy pierwszym połaczeniu
            {
                pow = 10;
                session_draw = randomInt();                         //losujemy inta z przedziału 0 do 2047
                Console.WriteLine("Uzyskano nową sesje o numerze: " + session_draw);

                for (int i = 133, j=0; i < 144 && j<SessionCheck.Count; i++, j++)
                {
                    if (Math.Pow(2, pow) <= session_draw)              //kodujemy go binarnie
                    {
                        bitArray[i] = true;
                        session_draw -= (int)Math.Pow(2, pow);
                    }
                    SessionCheck[j] = bitArray[i];
                    pow--;
                    if (i == 143)
                        Console.WriteLine("Zakończenie kodowania sesji");
                }
                FeedbackMessage.history_count = 0;



            }
            else if (session_result == 0 && bitArray.Count == 112)
            {
                pow = 10;
                session_draw = randomInt();                         //losujemy inta z przedziału 0 do 2047
                Console.WriteLine("Uzyskano nową sesje o numerze: " + session_draw);

                for (int i = 101, j = 0; i < 112 && j < SessionCheck.Count; i++, j++)
                {
                    if (Math.Pow(2, pow) <= session_draw)              //kodujemy go binarnie
                    {
                        bitArray[i] = true;
                        session_draw -= (int)Math.Pow(2, pow);
                    }
                    SessionCheck[j] = bitArray[i];
                    pow--;
                    if (i == 111)
                        Console.WriteLine("Zakończenie kodowania sesji ");
                }
                FeedbackMessage.history_count = 0;
            }
            else if (session_result == 0 && bitArray.Count == 80)
            {
                pow = 10;
                session_draw = randomInt();                         //losujemy inta z przedziału 0 do 2047
                Console.WriteLine("Uzyskano nową sesje o numerze: " + session_draw);

                for (int i = 69, j =0; i < 80 && j<SessionCheck.Count; i++, j++)
                {
                    if (Math.Pow(2, pow) <= session_draw)              //kodujemy go binarnie
                    {
                        bitArray[i] = true;
                        session_draw -= (int)Math.Pow(2, pow);
                    }
                    SessionCheck[j] = bitArray[i];
                    pow--;
                    if (i == 79)
                        Console.WriteLine("Zakończenie kodowania sesji");
                }
                FeedbackMessage.history_count = 0;
            }
           

        }
    }
}
