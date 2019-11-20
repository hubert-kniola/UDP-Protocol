using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServerAlfaCSharp
{

    class CalculationsClas
    {

        //public static long result_value;
        public static void Calculation(int[] val_1, int[] val_2, BitArray bitArray /* odebrany BIArray*/)
        {
            if (bitArray[0] == false && bitArray[1] == true && bitArray[2] == false && bitArray[3] == false && bitArray[4] == false && val_2[0] == 0)
            {
                sendingErrorAboutDivisionByZero(bitArray);
            }
            else
            {
                long result_value = 0;
                SessionClass.session(bitArray);                                     //Nadanie sesji 
                if (bitArray[2] == false && bitArray[3] == false && bitArray[4] == false)               //Operacje + - * /
                {
                    if (bitArray[0] == false && bitArray[1] == false)
                        result_value = multiOperation(val_1[0], val_2[0]);
                    else if (bitArray[0] == false && bitArray[1] == true)
                        result_value = diviOperation(val_1[0], val_2[0]);
                    else if (bitArray[0] == true && bitArray[1] == false)
                        result_value = addOperation(val_1[0], val_2[0]);
                    else if (bitArray[0] == true && bitArray[1] == true)
                    {
                        result_value = subOperation(val_1[0], val_2[0]);
                        if (val_1[0] < val_2[0])
                            bitArray[3] = true;
                    }
                }
                else if (bitArray[2] == false && bitArray[3] == false && bitArray[4] == true)           //Silnia
                    result_value = factorialOperation(val_1[0]);

                if (result_value < 0)
                {
                    result_value *= -1;
                    bitArray[4] = true;
                }
                else if (result_value > int.MaxValue && (bitArray[0] == false && bitArray[1] == false || bitArray[0] == true && bitArray[1] == false))
                {

                    bitArray[2] = true;
                    bitArray[3] = true;
                    bitArray[4] = true;
                    result_value = 0;
                }

                FeedbackMessage.feedback((int)result_value, bitArray);
            }
        }

        static long addOperation(long firstValue, long secondValue)
        {
            long product;

            product = firstValue + secondValue;

            return product;
        }
        static int subOperation(int firstValue, int secondValue)
        {
            int product;
            if (firstValue >= secondValue)
            {
                product = firstValue - secondValue;
            }
            else
            {
                product = secondValue - firstValue;
            }
            return product;
        }

        static long multiOperation(int firstValue, int secondValue)
        {
            long product;

            product = firstValue * secondValue;

            return product;
        }

        static int diviOperation(int firstValue, int secondValue)
        {
            int product;
            if (firstValue >= secondValue)
            {
                product = firstValue / secondValue;
            }
            else
            {
                product = secondValue / firstValue;
            }
            return product;
        }

        static long factorialOperation(int value)
        {
            long result = 1;
            for (int i = 1; i <= value; i++)
            {
                result *= i;
            }
            return result;
        }

        static void sendingErrorAboutDivisionByZero(BitArray recvBitArray)
        {
            BitArray divisionByZero = new BitArray(80, false);
            divisionByZero[1] = divisionByZero[2] = divisionByZero[3] = divisionByZero[4] = true; //kodowanie błedu 01 111
            divisionByZero[65] = divisionByZero[67] = divisionByZero[68] = true; //kodowanie długości bitów (11)
            SessionClass.session(recvBitArray);                                     //Nadanie sesji 

            for (int i = recvBitArray.Count - 11, j = divisionByZero.Count - 11; i < recvBitArray.Count && j < divisionByZero.Count; i++, j++)
                divisionByZero[j] = recvBitArray[i];

            byte[] sendByteToTheClient = new byte[divisionByZero.Count / 8];
            sendByteToTheClient = CoddingClass.CoddingBitToByteArray(divisionByZero);
            SendClass.SendMessageToTheClient(sendByteToTheClient);                              //Wysłanie wiadomośći do klineta
            Console.WriteLine("Wiadomosć została wysłana");
        }
    }
}
