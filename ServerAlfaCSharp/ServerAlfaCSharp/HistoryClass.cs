using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{
    class HistoryClass
    {
        public BitArray variable_1 = new BitArray(32);
        public BitArray variable_2 = new BitArray(32);
        public BitArray result = new BitArray(32);
        public BitArray session = new BitArray(11);
        public BitArray operation_ID = new BitArray(2);

        public HistoryClass()
        {
            variable_1 = new BitArray(32, false);
            variable_2 = new BitArray(32, false);
            result = new BitArray(32, false);
            session = new BitArray(11, false);
            BitArray operation_ID = new BitArray(2, false);
        }

        public static HistoryClass SaveStories(BitArray recevBitArray, BitArray SendSendBitArrayToTheClinet)
        {
            HistoryClass temp = new HistoryClass();

            if (recevBitArray[2] == false && recevBitArray[3] == false && (recevBitArray[4] == false || recevBitArray[4] == true))
            {
                for (int i = 0; i < 2; i++)                                                                             //ID operation
                    temp.operation_ID[i] = recevBitArray[i];
                for (int i = 0, j = 101; i < temp.session.Count && j < SendSendBitArrayToTheClinet.Count; i++, j++)     //Sesja
                    temp.session[i] = SendSendBitArrayToTheClinet[j];
                for (int i = 0, j = 69; i < temp.variable_1.Count && j < 101; i++, j++)                                 //Zmienna 1
                    temp.variable_1[i] = recevBitArray[j];
                for (int i = 0, j = 101; i < temp.variable_2.Count && j < 133; i++, j++)                                //Zmienna 2
                    temp.variable_2[i] = recevBitArray[j];
                for (int i = 0, j = 69; i < temp.result.Count && j < 101; i++, j++)                                     //wynik
                    temp.result[i] = SendSendBitArrayToTheClinet[j];
                
            }
            return temp;

        }

        public static void TransformBit144toBit80Array(BitArray recevBit)
        {

            BitArray historyRequest = new BitArray(80, false);
            for (int i = 0; i < historyRequest.Length; i++)
                historyRequest[i] = recevBit[i];

            FeedbackMessage.SendHistory(historyRequest);
        }

        public static void TransformBit144toBit112Array(BitArray recevBit)
        {

            BitArray historyRequest = new BitArray(112, false);
            for (int i = 0; i < historyRequest.Length; i++)
                historyRequest[i] = recevBit[i];

            FeedbackMessageID.FeedbackHistryID(historyRequest);
        }

    }
}
