using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFishController : FishesType
{
    
   public static int lifePoints = 2;
   private int damage = 0; //numero di tap subiti
    
    /* gestione danno dello squalo */
    override protected void OnMouseDown() {
        damage ++; 
        if(damage == lifePoints)
           Kill();
    }
}
