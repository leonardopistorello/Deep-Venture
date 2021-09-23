using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{   
    /* modifica a public */
    public static GameController SharedInstance; 
    private void Awake()
    {
        SharedInstance = this;
    }


    
   
    //-----------------
    public int bombs = 3;
    public int lives = 3;
    public int score = 0;
    public Button RestartButton;
    public Button ExitButton;
    public GameObject UIpanel;
    public static int gameState; //stato del gioco
    

    public static void AddScore(int score)
    {
        score += score;
    }
     
    public static void DetonateBomb()
    {
        if (AreBombsAvailable() && IsGameRunning())
        {
            GameController.SharedInstance.bombs--;
            GameController.SharedInstance.KillAllFishes();
        }
    }
    public static void SpendLives(int lives)
    {
        lives -= lives;
    }
    public static bool IsGameRunning()
    {
        return gameState == 1;
    }
    public static bool IsGameOver() {
        return gameState == 0;
    }
    public static bool AreBombsAvailable()
    {
        return GameController.SharedInstance.bombs > 0;
    }
    public static void RestartGame()
    {
        SceneManager.LoadScene("TapPanic");
    }

    public void GameOver() {
         gameState = 0;
        
    }
    //------------------------
    private void KillAllFishes()
    {
        foreach (FishesType f in FindObjectsOfType<FishesType>())
        {
                f.Kill();
        }
    }
    //----------------------
    void Start()
    {
        // setting game to run normally
        UIpanel.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        Time.timeScale = 1;
        gameState = 1;
        
    }
    void Update()
    {
        switch (gameState)
        {
            case 1:
                
                if (ProgressBarController.Instance.IsEmpty())
                {
                    GameOver();
                }
                break;
            case 0:
                Time.timeScale = 0; // hard pausing the game if it is not running
                UIpanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
