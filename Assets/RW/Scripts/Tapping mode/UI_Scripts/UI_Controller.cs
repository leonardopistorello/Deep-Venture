using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
   
    public void StartJelly()
    {
        SceneManager.LoadScene("Jellyfish");
    }

     public void StartTapPanic()
    {
        SceneManager.LoadScene("TapPanic");
    }

    public void Restart() {
        SceneManager.LoadScene("TapPanic");
    }

    public void Exit() {
        SceneManager.LoadScene("Main Menu");
    }
}
