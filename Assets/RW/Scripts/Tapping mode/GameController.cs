using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance;
    public static GameController Instance
    {
        get { if (instance == null) instance = FindObjectOfType<GameController>(); return instance; }
        private set { instance = value; }
    }
    //-----------------
    private int bombs = 3;
    private int lives = 3;
    private int score = 0;
    private GameState gameState;
    public static GameState GameState
    {
        get { return Instance.gameState; }
        private set { Instance.gameState = value; }
    }
    private float elapsedTime = 0f;

    public static int Score
    {
        get { return Instance.score; }
        private set { Instance.score = value; }
    }
    public static int Lives
    {
        get { return Instance.lives; }
        private set { Instance.lives = value; }
    }
    public static int Bombs
    {
        get { return Instance.bombs; }
        private set { Instance.bombs = value; }
    }

    public static void AddScore(int score)
    {
        Score += score;
    }
     public static float GetElapsedTime()
    {
        return Instance.elapsedTime;
    }
    public static void DetonateBomb()
    {
        if (AreBombsAvailable() && IsGameRunning())
        {
            Bombs--;
            Instance.KillAllFishes();
        }
    }
    public static void SpendLives(int lives)
    {
        Lives -= lives;
    }
    public static bool IsGameRunning()
    {
        return GameState == GameState.Running;
    }
    public static bool AreBombsAvailable()
    {
        return Bombs > 0;
    }
    public static void RestartGame()
    {
        SceneManager.LoadScene("Game");
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
        Time.timeScale = 1;
        gameState = GameState.Running;
    }
    void Update()
    {
        switch (gameState)
        {
            case GameState.Running:
                // check if round has ended
                elapsedTime += Time.deltaTime;
                if (lives <= 0)
                {
                    gameState = GameState.GameOver;
                }
                break;
            case GameState.WinScreen:
                Time.timeScale = 0; // hard pausing the game if it is not running
                break;
            case GameState.GameOver:
                Time.timeScale = 0; // hard pausing the game if it is not running
                break;
            default:
                break;
        }
    }
}
public enum GameState { Running, WinScreen, GameOver }