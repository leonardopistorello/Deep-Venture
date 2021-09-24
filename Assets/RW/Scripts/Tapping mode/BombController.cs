using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
   public float min_speedX = 1f;
   public float max_speedX = 3f;
   public float min_speedY = 5f;
   public float max_speedY = 12f;
   private Vector3 movement_x = Vector3.zero;
   private Vector3 movement_y = Vector3.zero;

    private void Start() {
       movement_x = new Vector3(-1f,0f,0f) * Random.Range(min_speedX,max_speedX);  
       movement_y = new Vector3(0,-1f,0f) * Random.Range(min_speedY,max_speedY);
       Debug.Log("Start");      
   }

    void Update()
    {
        transform.position += (movement_y + movement_x) * Time.deltaTime;
        if (transform.position.y > 4.5f || transform.position.y < -4.5f)
            movement_y = -movement_y;
   }

    private void OnMouseDown() 
    {
        // DestroyAllGameObjects();
        AudioSource bombaMetallica = this.gameObject.GetComponent<AudioSource>();
        bombaMetallica.Play();
        Debug.Log("GameOver()");
        GameController.SharedInstance.GameOver();
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