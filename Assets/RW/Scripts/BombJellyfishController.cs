using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombJellyfishController : MonoBehaviour
{
    public float speedY = 5f;
    private Vector3 movement_y = Vector3.zero;
    private Collider2D bombCollider;

    private void Start()
    {
        bombCollider = gameObject.GetComponent<Collider2D>();
        movement_y = new Vector3(0, -1f, 0f) * speedY;
        Debug.Log("Start");
    }

    void Update()
    {
        transform.position += movement_y  * Time.deltaTime;
        if (transform.position.y > 4.5f || transform.position.y < -4.5f)
            movement_y = -movement_y;
    }

    /*private void OnMouseDown()
    {
        // DestroyAllGameObjects();
        this.gameObject.SetActive(false);
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
    }*/


}
