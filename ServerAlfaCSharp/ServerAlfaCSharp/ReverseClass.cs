using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{
    class ReverseClass
    {
        public static BitArray reverseOrder(BitArray temp)
        {
            int value = temp.Count - 1;
            BitArray newOne = new BitArray(temp.Count);
            for (int i = 0; i < temp.Count; i++)
            {
                newOne[i] = temp[value];
                value--;
            }
            return newOne;
        }
    }
}
