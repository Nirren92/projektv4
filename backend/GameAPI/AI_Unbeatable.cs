using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI
{
    public class AI_Unbeatable
    {
        

         public (int bestMove, int score) CalcMove( Game game, bool AI_turn, int level)
        {
            //kontroll om någon har vunnit. 
            if(game.Game_Done)
            {
                // har nån vunnit så retuneras det samt tar bort nivån för att kunna priotera tex en vinst eller blockering, -1 indikerar att inget drag är möjligt. dvs att spelet är slut. 
                if (game.Game_winner == "X") 
                {
                    return (-1, -(game.Plane.Length+1) + level);; 
                }
                else if (game.Game_winner == "O") 
                {
                    return (-1, (game.Plane.Length+1) - level); 
                    
                }
                //oavgjort
                else if (game.Game_winner == "D")
                {
                    return (-1, 0);
                }
            }
             //ingen har vunnit. går vidare för att kontrollera nästa möjliga drag dvs att en till nivå kommer bli kontrollerad. 
           
            //Score som används för att beräkna vilken väg i trädet som är bäst. intiterar beroende om det är spelare eller AI som ska köra. kontrollerar vilkens tur det
            // beroende på vems tur det är så skrivs ett högt eller lågt tal. detta för att kunna avgöra om det är bästa draget. 
            int score = AI_turn ? int.MinValue : int.MaxValue;
            int bestMove = -1; 
            //loopar igenom alla tommar rutor och sätter in draget baserat på vems tur det är.
            for (int i = 0; i < game.Plane.Length; i++)
            {
                //kontrollerar om det är tomt. isåfall skapas scenarion baserat på denna plats. 
                if(game.Plane[i] == "-")
                {
                    if (AI_turn)
                    {
                        game.Plane[i] = "O";    
                    }
                    else
                    {
                        game.Plane[i] = "X";
                    }
                    // skickar in ny spelplan i Calcmove för att kunna basera en ny nivå på hur spelplanen nu ser
                     var (tempMove, tempScore) = CalcMove(new  Game(game.Plane),!AI_turn, level+1);
                    //plockar bort mitt insatta värde 
                    game.Plane[i] = "-";
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