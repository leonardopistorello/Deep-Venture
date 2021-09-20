using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSubmarineController : MonoBehaviour
{
    public int speed = 3;
    private Rigidbody2D rigidBody; 
    private Vector3 bottom = new Vector3(-7.731f,-2.7f,0f); // posizione più bassa raggiungibile dal sottomarino
    private Vector3 upper = new Vector3(-7.731f,2.7f,0f); // posizione più alta raggiungibile dal sottomarino
    private Vector3 movement_y; 
    public bool isDead;
    public float forwardMovementSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-7f,0f,0f); //posizione iniziale del sottomarino
        movement_y = new Vector3(0,-1f,0f) * speed;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (transform.position.y < bottom.y || transform.position.y > upper.y) {
            movement_y = -movement_y;
        }
        transform.position += Time.deltaTime * movement_y;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
                Vector2 newVelocity = rigidBody.velocity;
                newVelocity.x = forwardMovementSpeed;
                rigidBody.velocity = newVelocity;
        }

        /*if (isDead)
        {
            restartButton.gameObject.SetActive(true);
        } */
    }
    
}
