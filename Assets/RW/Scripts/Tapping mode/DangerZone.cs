using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    
    public float forwardMovementSpeed = 5.0f;
    private Vector3 movement_x; 

    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-6.21f,0f,0f); //posizione iniziale 
         movement_x = new Vector3(1f,0f,0f) * forwardMovementSpeed;
         
    }

      public void OnTriggerEnter2D(Collider2D other)  {
        Debug.Log("onCollision");
        //Debug.Log(otherCollider.GameObject.name());
        GameController.SharedInstance.GameOver();
        Debug.Log(GameController.gameState);
      } 

    void Update()
    {
        
        transform.position += Time.deltaTime * movement_x;  
        
    }

   
}
