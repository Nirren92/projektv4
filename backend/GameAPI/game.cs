using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace GameAPI
{
    public class Game
    {
        public string[] Plane { get; set; }
        public bool Game_Done = false;
        public string Game_winner="";
        //skapar mitt spel. 
        public Game(string[] plane)
        {
            //validerar data
            ValidatePlane(plane); 
            this.Plane = plane;
            //kontrollerar om någon har vunnit. 
            CheckGame();
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
                    throw new ArgumentException("Spelplanen får endast innehålla X, O eller -. tomma platser markeras med -");
                }
            }
        }  
        private void CheckGame()
        {
            //möjliga vinstscenarion i list 
          List<int[]> WinCombo = new List<int[]>
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
            foreach (var numbers in WinCombo)
            {
                if(this.Plane[numbers[0]] == "X" && this.Plane[numbers[1]]=="X" && this.Plane[numbers[2]]=="X")
                {
                    this.Game_Done = true;
                    this.Game_winner = "X";
                }
                else if(this.Plane[numbers[0]] == "O" && this.Plane[numbers[1]]=="O" && this.Plane[numbers[2]]=="O")
                {
                    this.Game_Done = true;
                    this.Game_winner = "O";
                }
            }
            //kontroll om det är oavgjort. 
             if (!this.Plane.Contains("-"))
             {
                    this.Game_Done = true;
                    this.Game_winner = "D";
             }
        }
    }
}