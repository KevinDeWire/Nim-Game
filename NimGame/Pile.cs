using System;
using System.Text;


namespace NimGame
{
    public class Pile                                          // Represents the number of sticks in a pile as an integer and as a binary number
    {
        private int totalSticks;
        private int[] binaryBits = new int[8];          // Sets up an array to represent the bits of a binary number.
                                                        // The index is the power of 2. 2^index
                                                        // binaryBit[0] = 1 bit, binaryBit[1] = 2 bit, binaryBit[4] = 16 bit,...etc

        public Pile()                                   //Initalizes all values to 0
        {
            totalSticks = 0;
            BinaryConvert(0);
        }

        public Pile(int number)                         // Initalizes all values to the integer provided (1 - 255)
        {
            totalSticks = number;
            BinaryConvert(number);
        }

        private void BinaryConvert(int number)
        {
            int remainingNumber = number;
            int bitValue;

            for (int i = 7; i >= 0; i--)                // Starts at the 128 bit and works down to the 1 bit
            {
                bitValue = (int)Math.Pow(2,i);          // Calculates the value of the current bit and explicitly converts it to an integer
                if (remainingNumber >= bitValue)        // Sets the current bit to 1 and subtracts from the total
                {
                    binaryBits[i] = 1;
                    remainingNumber = remainingNumber - bitValue;
                }
                else                                    // Sets the current bit to 0
                    binaryBits[i] = 0;
            }
        }

        public int TotalSticks                          // A get / set method for totalSticks
        {
            get
            {
                return totalSticks;
            }
            set                                         // Automaticlly recalculates the binaryBits
            {
                totalSticks = value;
                BinaryConvert(value);
            }
        }

        public int BinaryBit(int index)                 // Read only access to the BinaryBits
        {
            return binaryBits[index]; 
        }



    }
}
