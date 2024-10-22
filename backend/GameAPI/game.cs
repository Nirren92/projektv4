using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI
{
    public class Game
    {
        public string[] Plane { get; set; }
    
        
        public Game() {}

        //skapar mitt spel. 
        public Game(string[] plane)
        {
            ValidatePlane(plane); 
            //sparar det även i en string array ifall jag hinner ändra. 
            this.Plane = plane;
        }

            private void ValidatePlane(string[] plane)
                {
                    if (plane == null || plane.Length != 9)
                    {
                        throw new ArgumentException("Dålig indata. Kontrollera spelplanen som skickades.");
                    }

                    foreach (string c in plane)
                    {
                        if (c != "X" && c != "O" && c != "-")
                        {
                            throw new ArgumentException("Spelplanen får endast innehålla X, O eller -.");
                        }
                    }
                }
                
        public string CheckGame()
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
                if(this.Plane[numbers[0]] == "X" && this.Plane[numbers[1]]=="X" && this.Plane[numbers[2]]=="X")
                {
                    return "X";
                }
                else if(this.Plane[numbers[0]] == "O" && this.Plane[numbers[1]]=="O" && this.Plane[numbers[2]]=="O")
                {
                    return "O";
                }
            }

            //kontroll om det är oavgjort. 
             if (!this.Plane.Contains("-"))
             {
                return "D";
             }

            return "-";
        
        }
    }
}