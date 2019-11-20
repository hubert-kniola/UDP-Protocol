using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
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

        public static void WriteHistoryClass(ref HistoryClass[] history, int history_count)
        {
            Console.WriteLine("W historii powinno być {0} elementów: ", history_count);
            for (int i = 0; i < history_count; i++)
            {
                Console.Write("Wynik {0}:", i);
                WriteBitTab(history[i].result);
            }
        }
    }
}
