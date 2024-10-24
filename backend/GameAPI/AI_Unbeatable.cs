using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI
{
    public class AI_Unbeatable
    {
        

         public (int bestMove, int score) CalcMove( string [] plane, bool AI_turn, int level)
        {
            //Skapar ett objekt med min spelplan
            Game checkwinners = new Game(plane);

            //kontroll om någon har vunnit. 
            string winner = checkwinners.CheckGame();
            // har nån vunnit så retuneras det samt tar bort nivån för att kunna priotera tex en vinst eller blockering, -1 indikerar att inget drag är möjligt. dvs att spelet är slut. 
            if (winner == "X") 
            {
                return (-1, -(plane.Length+1) + level);; 
            }
            else if (winner == "O") 
            {
                return (-1, (plane.Length+1) - level); 
                
            }
            //oavgjort
            else if (winner == "D")
            {
                return (-1, 0);
            }

             //ingen har vunnit. går vidare för att kontrollera nästa möjliga drag
           
            //Score som används för att beräkna vilken väg i trädet som är bäst. intiterar beroende om det är spelare eller AI som ska köra. 
            int score = AI_turn ? int.MinValue : int.MaxValue;
            int bestMove = -1; 

            for (int i = 0; i < plane.Length; i++)
            {

                //kontrollerar om det är tomt. isåfall skapas scenarion baserat på denna plats. 
                if(plane[i] == "-")
                {
                    if (AI_turn)
                    {
                        plane[i] = "O";    
                    }
                    else
                    {
                        plane[i] = "X";
                    }
                 
                    // skickar in ny spelplan i Calcmove för att kunna basera en ny nivå på hur spelplanen nu ser
                     var (tempMove, tempScore) = CalcMove(plane,!AI_turn, level+1);
                    //plockar bort mitt insatta värde 
                    plane[i] = "-";
                    //Sparar den med bäst poäng. kontrollerar både om det finns möjlighet för vinst eller om AI ska blocka spelare. 
                    if(AI_turn)
                    {
                        if(tempScore > score)  
                        {
                            score = tempScore;
                            bestMove = i;
                        }
                    }
                    else
                    {
                         if(tempScore < score)  
                        {
                            score = tempScore;
                            bestMove = i;
                        }
                    }
                }
            }


            return (bestMove, score);;
        }










    
    }
}