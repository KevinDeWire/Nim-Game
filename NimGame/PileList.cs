using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NimGame
{
    public class PileList
    {
        public List<Pile> pileList;
        
        public PileList()
        {                                           
           pileList = new List<Pile>();
        }
        public void Add(int numOfSticks)
        {
            Pile newPile = new Pile(numOfSticks);
            pileList.Add(newPile);
        }
        public int LargestPile()
        {
            int largest = 0;

            for (int i = 1; i < pileList.Count; i++)
            {
                if(pileList[i].TotalSticks > pileList[largest].TotalSticks)
                    largest = i;
            }
            return largest;
        }

        public void PrintPiles()
        {
            for (int i = 0; i < pileList.Count; i++)
                Console.Write("Pile " + (i + 1) + " = " + pileList[i].TotalSticks.ToString() + " | ");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
