using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    GameController gameContr;

   void Start() {
        gameContr = GameController.Instance;
   }
    
    public void OnTriggerEnter2D(Collider2D otherCollider)  {
        gameContr.GameOver();
    }

  

   
}
