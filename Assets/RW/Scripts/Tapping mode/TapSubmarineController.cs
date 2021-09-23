using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSubmarineController : MonoBehaviour
{
    public int speed_y = 3;
    private Vector3 bottom = new Vector3(-7.731f,-2.7f,0f); // posizione più bassa raggiungibile dal sottomarino
    private Vector3 upper = new Vector3(-7.731f,2.7f,0f); // posizione più alta raggiungibile dal sottomarino
    private Vector3 movement_y; 
    private Vector3 movement_x; 

    public bool isDead;
    public float forwardMovementSpeed = 5.0f;
    
    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-8f,0f,0f); //posizione iniziale del sottomarino
        movement_y = new Vector3(0,-1f,0f) * speed_y;
        movement_x = new Vector3(1f,0f,0f) * forwardMovementSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (transform.position.y < bottom.y || transform.position.y > upper.y) {
            movement_y = -movement_y;
        }
        transform.position += Time.deltaTime * (movement_y+movement_x);
        

       
    }



        /* if (isDead)
        {
            GameController.instance.GameOver();
        } 
        */

        
    }
    

