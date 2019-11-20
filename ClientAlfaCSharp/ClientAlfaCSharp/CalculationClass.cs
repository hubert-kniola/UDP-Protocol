using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClientAlfaCSharp
{
    class CalculationClass
    {
        public static int bitSendLenght = 144;
        static string val_1, val_2;
        static int[] byte_1 = new int[1];
        static int[] byte_2 = new int[1];


        public static void Calculation(string action, ref byte[] dataSend, ref BitArray sessionBitArray)
        {
            int checkSession = 0;
            double pow = 10;
            BitArray bitArrayToCode = new BitArray(bitSendLenght, false);                             //MAIN BIT -144 elementowa tablica bitów


        poczatek:
            Console.Write("Podaj liczbe 1 i 2: ");
            val_1 = Console.ReadLine();
            val_2 = Console.ReadLine();
            char nowy;
            int checkValue1 = 0;
            int checkValue2 = 0;

            for (int i = 0; i < val_1.Length; i++)
            {
                nowy = val_1[i];
                if (Char.IsDigit(nowy) == false && nowy != '-')
                    checkValue1++;
            }

            for (int i = 0; i < val_2.Length; i++)
            {
                nowy = val_2[i];
                if (Char.IsDigit(nowy) == false && nowy != '-')
                    checkValue2++;
            }


            if (checkSession == 0)
                for (int i = 0; i < sessionBitArray.Count; i++)                                           //Sprawdzanie czy sesja została już nadana
                {
                    if (sessionBitArray[i] == true)
                        checkSession += (int)Math.Pow(2, pow);
                    pow--;
                }

            if (checkSession == 0)                                                                    //Nadanie sesji
                for (int i = 0, j = 133; i < sessionBitArray.Count && j < bitArrayToCode.Count; i++, j++)
                {
                    bitArrayToCode[j] = sessionBitArray[i];
                }

            if (checkValue1 > 0 || checkValue2 > 0)
            {
                Console.WriteLine("Proszę wprowadzić same cyfry!");
                goto poczatek;
            }
            else if (val_1 == "" || val_2 == "" || long.Parse(val_1) < 0 || long.Parse(val_1) > int.MaxValue || long.Parse(val_2) < 0 || long.Parse(val_2) > int.MaxValue)
            {
                Console.WriteLine("Liczba z poza przedziału! Dostępny przedział to <0,{0}>", int.MaxValue);
                goto poczatek;
            }
            //else if (action == "dz" && int.Parse(val_2) == 0)
            //{
            //    Console.WriteLine("Błąd dzielenia przez 0!");
            //    goto poczatek;
            //}
            else if (action == "dz" && int.Parse(val_1) < int.Parse(val_2))
            {
                Console.WriteLine("System operuje na liczbach całkowitych, zatem proszę podac dzielnik mniejszy od licznika!");
                goto poczatek;
            }
            else if (action == "o" && int.Parse(val_1) < int.Parse(val_2))
            {
                Console.WriteLine("System operuje na liczbach dodatnich, zatem wartość druga nie może być większa od wartości pierwszej!");
                goto poczatek;
            }
            else if (checkValue1 > 0 || checkValue2 > 0)
            {
                Console.WriteLine("Proszę wprowadzić same cyfry!");
                goto poczatek;
            }
            else    //operacje obliczania
            {
                //Kodowanie i odwracanie  val_1
                byte_1[0] = int.Parse(val_1);
                BitArray bit_1 = new BitArray(byte_1);
                bit_1 = ReverseClass.reverseOrder(bit_1);

                //Kodowanie i odwracanie val_2
                byte_2[0] = int.Parse(val_2);
                BitArray bit_2 = new BitArray(byte_2);
                bit_2 = ReverseClass.reverseOrder(bit_2);

                //Kodujemy ciąg 144 bitów 
                CoddingClass.CoddingBitArray(action, bit_1, bit_2, ref bitArrayToCode, sessionBitArray);
                //WriteClass.WriteBitTab(bitArrayToCode);

                dataSend = CoddingClass.CoddingBitToByteArray(bitArrayToCode);
            }
        }

        public static void factorial(string action, ref byte[] dataSend, ref BitArray sessionBitArray)
        {
            BitArray bitArrayToCode = new BitArray(bitSendLenght, false);
            int checkSession = 0;
            double pow = 10;
            char nowy;

        poczatek:
            int checkValue1 = 0;
            Console.Write("Podaj liczbe: ");
            val_1 = Console.ReadLine();

            for (int i = 0; i < val_1.Length; i++)
            {
                nowy = val_1[i];
                if (Char.IsDigit(nowy) == false && nowy != '-')
                    checkValue1++;
            }

            if (checkSession == 0)
                for (int i = 0; i < sessionBitArray.Count; i++)                                           //Sprawdzanie czy sesja została już nadana
                {
                    if (sessionBitArray[i] == true)
                        checkSession += (int)Math.Pow(2, pow);
                    pow--;
                }

            if (checkSession == 0)                                                                    //Nadanie sesji
                for (int i = 0, j = 133; i < sessionBitArray.Count && j < bitArrayToCode.Count; i++, j++)
                {
                    bitArrayToCode[j] = sessionBitArray[i];
                }
            if (checkValue1 > 0)
            {
                Console.WriteLine("Proszę wprowadzić same cyfry!");
                goto poczatek;
            }
            else if (val_1 == "" || long.Parse(val_1) < 0 || long.Parse(val_1) > int.MaxValue)
            {
                Console.WriteLine("Liczba z poza przedziału! Dostępny przedział to <0,{0}>", int.MaxValue);
                goto poczatek;
            }
            else if(int.Parse(val_1) >12)
            {
                Console.WriteLine("Wynik moze znadjdowac się poza zakresem zmiennej, podaj liczbe mniejsza niż 12!");
                goto poczatek;
            }
            else
            {
                //Kodowanie i odwracanie  val_1
                byte_1[0] = int.Parse(val_1);
                BitArray bit_1 = new BitArray(byte_1);
                bit_1 = ReverseClass.reverseOrder(bit_1);

                //Kodowanie i odwracanie val_2
                byte_2[0] = 0;
                BitArray bit_2 = new BitArray(byte_2);
                CoddingClass.CoddingBitArray(action, bit_1, bit_2, ref bitArrayToCode, sessionBitArray);
                dataSend = CoddingClass.CoddingBitToByteArray(bitArrayToCode);


            }
        }
    }
}
