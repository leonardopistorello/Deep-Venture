using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
   public static int lifePoints = 7;
   public float speed = 5f; 
   private int damage; //numero di tap subiti
   private Vector3 movement = Vector2.zero;
  
  /* set up: posizione iniziale dello squalo e 
   * incremento della posizione "movement */
   private void Start() {
       movement = new Vector3(1f,0f,0f) * speed; 
       transform.position = new Vector3(16f,0f,0f);
       
   }
  /* aggiornamento posizione dello squalo */
   private void Update() {
       transform.position += movement * Time.deltaTime;
      
   }
    
    /* gestione danno dello squalo */
    void OnMouseDown() {
        damage ++; 
        if(damage == lifePoints)
           kill();
    }
   /* uccisione dello squalo ed eliminazione del gameObject */
   public void kill() {
       Destroy(this.gameObject);
   }

}
