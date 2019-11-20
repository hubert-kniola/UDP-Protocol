using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{
    class DecoddingClass
    {
        public static BitArray decoddingByteToBitArray(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            byte[] tempByte = CoddingClass.CoddingBitToByteArray(bitArray);
            bitArray = new BitArray(tempByte);
            //bitArray = ReverseClass.reverseOrder(bitArray);
            //WriteClass.WriteBitTab(bitArray);
            return bitArray;
        }
    }
}
