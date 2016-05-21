//-----------------------------------------------------------------------------
// Program: Seminar 6 Group Project; Nim Game
// Description: Allows users to play a game of Nim against the computer.
//              The computer attempts to use Nim addition to gain a winning position.
// Inputs: The player determines the number of piles and the number of sticks in each pile.
// Outputs: The sticks in each pile, and messages to the player.
// Written by: Kevin DeWire
//             Kenneth Alcorn
// Last modification: 24 June 2015
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NimGame
{
    public class Program
    {
        static void Main(string[] args)             
        {
            int playerTurn;
            int maxPiles;
            bool repeat = true;
            bool gameOver;
            PileList pileList;
            

            Console.WriteLine("WELCOME TO NIM");
            Console.WriteLine();
            Console.WriteLine("The object of the game is to be the last person");
            Console.WriteLine("to remove at least one stick from the piles.");
            Console.WriteLine();
            Console.WriteLine("We will take turns until all of the sticks are gone.");
            Console.WriteLine();
            do  // Start the game loop
            {
                gameOver = false;
                pileList = new PileList();

                Console.WriteLine("Would you like to go first? y/n");       // Determine first player
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine();
                    playerTurn = 1;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Then I will go first.");
                    Console.WriteLine();
                    playerTurn = 2;
                }

                maxPiles = MaxPiles();              //Build the piles
                BuildPiles(pileList, maxPiles);

                do      // Start the turns loop
                {
                    pileList.PrintPiles();
                    if (playerTurn == 1)    //Players turn
                    {
                        PlayerTurn(pileList, maxPiles);
                        gameOver = WinCondition(pileList, maxPiles);
                        if(gameOver)
                        {
                            Console.WriteLine("CONGRATULATIONS !! YOU WIN!!");
                            Console.WriteLine();
                        }
                        else
                            playerTurn = 2;    
                    }
                    else                    // Computers turn
                    {
                        ComputerTurn(pileList, maxPiles);
                        gameOver = WinCondition(pileList, maxPiles);
                        if (gameOver)
                        {
                            Console.WriteLine("TOO BAD, I WIN.");
                            Console.WriteLine();
                        }
                        else
                            playerTurn = 1;
                    }
                } 
                while (!gameOver);      // End the turns loop

                Console.WriteLine("Would you like to play again? y/n");       // Play again???
                if (Console.ReadLine().ToLower() != "y")
                {
                    repeat = false;
                    Console.WriteLine(); 
                    Console.WriteLine("Thank you for playing.");
                    Console.WriteLine("I will see you next time.");
                    Console.WriteLine();
                    Console.ReadLine();
                }
            } 
            while (repeat);     // End of the game loop
        }

        

        //
        // Pile building starts here
        //
        public static int MaxPiles()
        {
            int numOfPiles = 0;
            do
            {
                try
                {
                    Console.WriteLine("How many piles would you like to have?");
                    numOfPiles = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    numOfPiles = 0;
                }
                Console.WriteLine();
            }
            while (numOfPiles == 0);
            return numOfPiles;
        }

        public static void BuildPiles(PileList list, int maxPiles)
        {
            int numOfSticks;

            for(int i = 0; i < maxPiles; i++)
            {
                numOfSticks = 0;
                do
                {
                    try
                    {
                        Console.WriteLine("How many sticks would you like to have in pile " + (i + 1) + "?");
                        numOfSticks = Int32.Parse(Console.ReadLine());
                     }
                    catch
                    {
                        numOfSticks = 0;
                    }
                    Console.WriteLine();
                }
                while (numOfSticks == 0);

                list.Add(numOfSticks);
            }
        }
        //
        // Pile building ends here
        //



        //
        // Player turn starts here
        //
        static void PlayerTurn(PileList list, int maxPiles)
        {
            int workingPile = 0;
            int newSticKTotal;
            int numRemoved = 0;

            do
            {
                try
                {
                    Console.WriteLine("Which pile would you like to remove sticks from? ");
                    workingPile = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine();
                    Console.WriteLine("How many sticks would you like to remove from this pile? ");
                    numRemoved = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch 
                {
                    Console.WriteLine("That is not a valid option. Please try again.");
                    Console.WriteLine();
                    workingPile = 0;
                    numRemoved = 0;
                }

                if (workingPile < 0 || workingPile >= list.pileList.Count)
                {
                    Console.WriteLine("That is not a valid pile.");
                    Console.WriteLine();
                    workingPile = 0;
                    numRemoved = 0;
                }

                if (numRemoved > list.pileList[workingPile].TotalSticks)
                {
                    Console.WriteLine("That is too many sticks.");
                    Console.WriteLine();
                    workingPile = 0;
                    numRemoved = 0;
                }

            }
            while (numRemoved == 0);

            newSticKTotal = list.pileList[workingPile].TotalSticks - numRemoved;
            list.pileList[workingPile].TotalSticks = newSticKTotal;
        }
        //
        // Player turn ends here
        //



        //
        // Computer game logic starts here
        //
        static void ComputerTurn(PileList list, int maxPiles) // All of the actions required by the computer to remove sticks from a pile
        {
            int firstBit;       // The largest bit that returns a 1 after Nim Add
            int workingPile;    // The pile that will have sticks removed
            int newTotalSticks; // The new number of sticks in the working pile after removing some

            firstBit = GetFirstBit(list, maxPiles); // Call to find the first Nim Add bit equal to 1
            
            if (firstBit == -1)
            {
                workingPile = list.LargestPile();   // If the Nim Add is all 0's then use the largest pile (( NEED THE INDEX OF LARGEST PILE ))
            }
            else
            {
                workingPile = GetWorkingPile(list, maxPiles, firstBit); // Find the first pile with a 1 in the firstBit position
                if (workingPile == -1)
                {
                    workingPile = list.LargestPile();   // If a -1 is returned set the working pile to the largest pile (( NEED THE INDEX OF LARGEST PILE ))
                }
            }

            newTotalSticks = StickRemoval(list, workingPile, maxPiles);

            // Message to the player
            Console.WriteLine("I will remove " + (list.pileList[workingPile].TotalSticks - newTotalSticks).ToString() + " from pile " + (workingPile + 1).ToString() + ".");
            Console.WriteLine();
            list.pileList[workingPile].TotalSticks = newTotalSticks;    // Sets the new total of sticks in the pile
            
        }
           
        static int GetFirstBit(PileList list, int maxPiles)    // Will return the first bit that Nim Adds to 1, or -1 if all bits Nim Add to 0
        {
            int currentBit = -1;
            int totalBit;
            
            for (int i = 7; i >= 0; i--)
            {
                totalBit = 0;
                for (int j = 0; j < maxPiles; j++)
                {
                    totalBit = totalBit + list.pileList[j].BinaryBit(i);
                }
                if ((totalBit % 2) == 1)
                {
                    currentBit = i;
                    return currentBit;  // This returns the index of the first bit that results in a Nim Add of 1
                }
            }
            return currentBit;  // This returns -1 if all Nim Adds resulted in 0
        }

        static int GetWorkingPile(PileList list, int maxPiles, int firstBit)    // Will determine which pile to remove sticks from
        {
            for (int i = 0; i < maxPiles; i++)
            {
                if (list.pileList[i].BinaryBit(firstBit) == 1)
                {
                    return i;   // This will return the index of the first pile found with a 1 in the firstBit position.
                }
            }
            return -1;  // If no pile is found a -1 is returned. This shouldn't happen.
        }

        static int StickRemoval(PileList list, int workingPile, int maxPiles)   // Will determine the number of sticks remaining in the pile
        {
            int remainTotal = 0;
            int bitTotal;

            for (int i = 7; i >= 0; i--)
            {
                bitTotal = 0;
                for (int j = 0; j < maxPiles; j++)
                {
                    if (j != workingPile)
                    {
                        bitTotal = bitTotal + list.pileList[j].BinaryBit(i);    // Calculates the total of the current bit for all piles
                    }
                }
                if ((bitTotal % 2) == 1)    // If the bit total equals 1, the value of the bit will be added to the remaining total
                {
                    remainTotal = remainTotal + (int)Math.Pow(2, i);    // Explicitly converts the value of the current bit and adds to total
                }
            }

            if (remainTotal == list.pileList[workingPile].TotalSticks)
                remainTotal = list.pileList[workingPile].TotalSticks / 2;

            return remainTotal; // This will be the number of sticks remaining in the pile

        }
        //
        // Computer game logic ends here
        //



        //
        // Win condition starts here
        //
        private static bool WinCondition(PileList list, int maxPiles)
        {
            bool gameOver = false;
            int totalSticks = 0;
            for(int i = 0; i < maxPiles; i++)
            {
                totalSticks = totalSticks + list.pileList[i].TotalSticks;
            }
            if(totalSticks == 0)
                gameOver = true;

            return gameOver;
        }
        //
        // Win condition ends here
        //
    }
}
