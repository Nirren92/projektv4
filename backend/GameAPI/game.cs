using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI
{
    public class Game
    {
        
        public char CheckGame(char [] plane)
        {
            //möjliga vinstscenarion i list 
          List<int[]> PossibleWinnings = new List<int[]>
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
                new int[] {0, 3, 6},
                new int[] {1, 4, 7},
                new int[] {2, 5, 8},
                new int[] {0, 4, 8},
                new int[] {2, 4, 6}
            };

            // kontrollerar om något scenario finns i spelplanen
            foreach (var numbers in PossibleWinnings)
            {
                if(plane[numbers[0]] == 'X' &&plane[numbers[1]]=='X' && plane[numbers[2]]=='X')
                {
                    return 'X';
                }
                else if(plane[numbers[0]] == 'O' &&plane[numbers[1]]=='O' && plane[numbers[2]]=='O')
                {
                    return 'O';
                }
            }

            //kontroll om det är oavgjort. 
             if (!plane.Contains('-'))
             {
                return 'D';
             }

            return '-';
        
        }
    }
}