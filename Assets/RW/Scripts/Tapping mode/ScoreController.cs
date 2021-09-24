using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;
    public int scoreValue;
    public static ScoreController instance;
  private void Awake()
     {
        instance = this;
     }
  public  void  UpdateScoreDisplay() 
   {
       score.text = "Score " + scoreValue.ToString();
   }

public void setScore(int newScore) 
{
    this.scoreValue = scoreValue + newScore;
}

}

