using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class FishesType : MonoBehaviour
{
    public float movementSpeed = 1f;
    protected Vector3 movement = Vector3.zero;

    virtual protected void Start()
    {
        movement = new Vector3(-1f, 0f, 0f) * movementSpeed; // going straight towards the camera
    }
    virtual protected void Update()
    {
        transform.position += movement * Time.deltaTime; //moving our fish
    }
    // main fish click detection
    virtual protected void OnMouseDown()
    {
        if (!GameController.IsGameRunning()) return;
        GameController.AddScore(1);
        Kill();
    }
    // main kill function
    public void Kill()
    {
        this.gameObject.SetActive(false); // destroying our fish
    }
}