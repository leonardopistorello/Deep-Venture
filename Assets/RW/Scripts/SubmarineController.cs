using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class SubmarineController : MonoBehaviour
{
    public float submarineForce = 75.0f; //Forza di spinta verso il basso del sottomarino
    public float forwardMovementSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    private bool isDead = false;
    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator submarineAnimator;
    public ParticleSystem submarine;
    private uint coins = 0;
    public Text coinsCollectedLabel;
    public Button restartButton;
    public AudioClip coinCollectSound;
    public AudioSource sonarAudio;
    public AudioSource engineAudio;

    public void RestartGame()
    {
        SceneManager.LoadScene("Jellyfish");
    }


    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else
        {
            HitByLaser(collider);
        }

    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isDead)
        {
            AudioSource electro = laserCollider.gameObject.GetComponent<AudioSource>();
            electro.Play();
        }
        isDead = true;
        submarineAnimator.SetBool("isDead", true);
    }

    void UpdateGroundedStatus()
    {
        //1
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        //2
        submarineAnimator.SetBool("isGrounded", isGrounded);
    }

    void AdjustSubmarine(bool submarineActive)
    {
        var submarineEmission = submarine.emission;
        submarineEmission.enabled = !isGrounded;
        if (submarineActive)
        {
            submarineEmission.rateOverTime = 300.0f;
        }
        else
        {
            submarineEmission.rateOverTime = 75.0f;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        submarineAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool submarineActive = Input.GetButton("Fire1");
        submarineActive = submarineActive && !isDead;

        if (submarineActive)
        {
            playerRigidbody.AddForce(new Vector2(0, submarineForce));
        }

        if (!isDead)
        {
                Vector2 newVelocity = playerRigidbody.velocity;
                newVelocity.x = forwardMovementSpeed;
                playerRigidbody.velocity = newVelocity;
        }
        UpdateGroundedStatus();
        AdjustSubmarine(submarineActive);

        if (isDead && isGrounded)
        {
            restartButton.gameObject.SetActive(true);
        }
    }
}
