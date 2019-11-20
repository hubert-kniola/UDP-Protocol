using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClientAlfaCSharp
{
    class WriteClass
    {
        public static void WriteBitTab(BitArray temp)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                Console.Write(temp[i] + " ");
                if ((i + 1) % 8 == 0)
                    Console.Write(" | ");
            }
            Console.WriteLine();
        }

        public static void WriteIntTab(int[] temp)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Write(temp[i] + " ");
                if ((i + 1) % 8 == 0)
                    Console.Write(" | ");
            }
            Console.WriteLine();
        }

        public static void WriteByteTab(byte[] temp)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Write(temp[i] + " ");
                if ((i + 1) % 8 == 0)
                    Console.Write(" | ");
            }
            Console.WriteLine();
        }
    }
}
