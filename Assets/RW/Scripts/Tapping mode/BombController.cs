using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombController : MonoBehaviour
{
   public float speed_x = 1f; 
   public float speed_y = 1f;
   private Vector3 movement_x = Vector3.zero;
   private Vector3 movement_y = Vector3.zero;

   private void Start() {
       movement_x = new Vector3(-1f,0f,0f) * speed_x; 
       movement_y = new Vector3(0,-1f,0f) * speed_y;
       transform.position = new Vector3(16f,0f,0f);
       Debug.Log("Start");      
   }

    void Update()
    {
        transform.position += (movement_y + movement_x) * Time.deltaTime;
        if (transform.position.y > 4.5f || transform.position.y < -4.5f)
            movement_y = -movement_y;
   }

    /* gestione danno dello squalo */
    private void OnMouseDown() 
    {
        DestroyAllGameObjects();
        Debug.Log("GameOver()");
    }

// Distrugge tutto purtroppo. Conviene fare un check ogni tanto e vedere in che ordine vengono storicizzati gli elementi.
     public void DestroyAllGameObjects()
 {
     GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
 
     for (int i = 0; i < GameObjects.Length; i++)
     {
        Debug.Log(GameObjects[i]);
         Destroy(GameObjects[i]);
     }
 }
}