using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}