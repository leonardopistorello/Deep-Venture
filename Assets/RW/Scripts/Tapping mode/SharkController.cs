using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : FishesType /*MonoBehaviour*/
{
   public static int lifePoints = 3;
   private int damage = 0; //numero di tap subiti
    
    /* gestione danno dello squalo */
    override protected void OnMouseDown() {
        damage ++; 
        if(damage == lifePoints) {
           damage = 0;
           Kill();
           ScoreController.instance.setScore(lifePoints);
           ScoreController.instance.UpdateScoreDisplay();

        }
    }

   
}
