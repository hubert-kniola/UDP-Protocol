using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{
    class CoddingClass
    {
        public static void CoddingBitArray(string action, BitArray val_1, BitArray val_2, ref BitArray BitTab)
        {
            if (action == "d")
                BitTab[0] = BitTab[1] = false;
            else if (action == "o")
                BitTab[1] = true;
            else if (action == "m")
                BitTab[0] = true;
            else if (action == "d")
                BitTab[0] = BitTab[1] = true;

            for (int i = 69, j = 0; i < 101 && j < val_1.Count; i++, j++) //Przypisywanie wartości 1
                BitTab[i] = val_1[j];
            for (int i = 101, j = 0; i < 133 && j < val_2.Count; i++, j++) //Przypisywanie wartości 2
                BitTab[i] = val_2[j];
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


        public static int[] CoddingBitToIntArray_1(ref int[] tabInt, BitArray bitArray)
        {
            double pow = 31;
            double result=0;
            for (int i = 69; i < 101; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }
            tabInt[0] = (int)result;
            return tabInt;
        }

        public static int[] CoddingBitToIntArray_2(ref int[] tabInt, BitArray bitArray)
        {
            double pow = 31;
            double result = 0;
            for (int i = 101; i < 133; i++)
            {
                if (bitArray[i] == true)
                    result += Math.Pow(2, pow);
                pow--;
            }

            tabInt[0] = (int)result;
            return tabInt;
        }

        private static double GetValue(double value)
        {
            return value;
        }
    }
}
