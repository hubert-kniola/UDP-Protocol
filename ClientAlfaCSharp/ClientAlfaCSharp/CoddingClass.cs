using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClientAlfaCSharp
{
    class CoddingClass
    {
        public static void CoddingBitArray(string action, BitArray val_1, BitArray val_2, ref BitArray BitTab, BitArray sessionBitArray)
        {
            if (action == "m")
                BitTab[0] = BitTab[1] = false;
            else if (action == "dz")
                BitTab[1] = true;
            else if (action == "d")
                BitTab[0] = true;
            else if (action == "o")
                BitTab[0] = BitTab[1] = true;
            else if (action == "s")
                BitTab[4] = true;


            for (int i = 69, j = 0; i < 101 && j < val_1.Count; i++, j++)                       //Przypisywanie wartości 1
                BitTab[i] = val_1[j];
            for (int i = 101, j = 0; i < 133 && j < val_2.Count; i++, j++)                      //Przypisywanie wartości 2
                BitTab[i] = val_2[j];
            for (int i = 133, j = 0; i < BitTab.Count && j < sessionBitArray.Count; i++, j++)   //Kodowanie sesji
                BitTab[i] = sessionBitArray[j];
            for (int i = 62; i < 69; i++)                                                       //Kodowanie długości bitów 75
                if (i == 62 || i == 65 || i == 67 || i == 68)
                    BitTab[i] = true;
        }

        public static void CoddingBitArrayHP(string action, BitArray val_1, ref BitArray BitTab, BitArray sessionBitArray)
        {
            if (action == "hsp")
                BitTab[2] = BitTab[4] = true;
            else if (action == "hip")
                BitTab[2] = BitTab[3] = true;

            for (int i = 69, j = 0; i < 101 && j < val_1.Count; i++, j++)                       //Przypisywanie wartości 1
                BitTab[i] = val_1[j];
            for (int i = 101, j = 0; i < BitTab.Count && j < sessionBitArray.Count; i++, j++)   //Kodowanie sesji
                BitTab[i] = sessionBitArray[j];

            BitTab[63] = BitTab[65] = BitTab[67] = BitTab[68] = true;                           //Kodowanie długości pola danych (43)



        }
        public static byte[] CoddingBitToByteArray(BitArray bitArray)
        {
            byte[] byteArray = new byte[bitArray.Count / 8];
            int incrementByte = 0;
            double pow = 7, value = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i] == true)
                    value += Math.Pow(2, pow);
                if ((i + 1) % 8 == 0)
                {
                    //Console.WriteLine(value);
                    byteArray[incrementByte] = (byte)value;
                    value = 0;
                    incrementByte++;
                }
                pow--;
                if (pow == -1)
                    pow = 7;
            }
            return byteArray;
        }

        public static int[] CoddingBitToIntArray(ref int[] tabInt, BitArray bitArray)
        {
            double pow = 31;
            double result = 0;
            for (int i = 69; i < 101; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }
            tabInt[0] = (int)result;
            return tabInt;
        }

        public static int[] CoddingBitToIntArrayHistory(BitArray bitArray)
        {
            int[] tabInt = new int[3];
            double pow = 31;
            double result = 0;
            for (int i = 69; i < 101; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
               
            }
            pow = 31;
            tabInt[0] = (int)result;
            result = 0;
            for (int i = 101; i < 133; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }
            pow = 31;
            tabInt[1] = (int)result;
            result = 0;
            for (int i = 133; i < 165; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }
            tabInt[2] = (int)result;
            result = 0;
            return tabInt;
        }

        public static void CoddingSession(ref BitArray bitArraySession11, BitArray recvBitArray/*80/112/176*/)
        {
            int session_number = 0;
            double pow = 10;
            for (int i = 0; i < bitArraySession11.Count; i++)
            {
                if (bitArraySession11[i] == true)
                    session_number += (int)Math.Pow(2, pow);
                pow--;
            }

            if (session_number == 0)
            {
                if (recvBitArray.Count == 80)
                    for (int i = 0, j = 69; i < bitArraySession11.Count && j < recvBitArray.Count; i++, j++)                            //Przypisanie sesji do tablicy wysyłanej do klineta
                        bitArraySession11[i] = recvBitArray[j];
                else if (recvBitArray.Count == 112)
                    for (int i = 0, j = 101; i < bitArraySession11.Count && j < recvBitArray.Count; i++, j++)                            //Przypisanie sesji do tablicy wysyłanej do klineta
                        bitArraySession11[i] = recvBitArray[j];
                else if(recvBitArray.Count==178)
                    for (int i = 0, j = 167; i < bitArraySession11.Count && j < recvBitArray.Count; i++, j++)                            //Przypisanie sesji do tablicy wysyłanej do klineta
                        bitArraySession11[i] = recvBitArray[j];
            }
            Console.WriteLine();
        }

        public static void SessionToIntArray(ref int[] intArray, BitArray bitArray)
        {
            int pow = bitArray.Count - 1;
            int result = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i] == true)
                    result += (int)Math.Pow(2, pow);
                pow--;
            }
            intArray[0] = result;
        }


        private static double GetValue(double value)
        {
            return value;
        }
    }
}
