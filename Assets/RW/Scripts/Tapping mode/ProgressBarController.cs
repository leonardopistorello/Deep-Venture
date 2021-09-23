using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarController : MonoBehaviour
{
    public static ProgressBarController Instance;
    public Image ProgressBar;
    
    /* crea un'unica istanza dell'oggetto ProgressBarController */
    private void Awake()
    {   
        if(Instance == null) {
            Instance = this; 

        }
         else if (Instance !=this) {
            Destroy(gameObject);
        }
    }

  

    /* la barra viene svuotata man mano che scorre il tempo */
    void Update()
    {
        if(ProgressBar.fillAmount > 0f) { 
           ProgressBar.fillAmount -= 0.001f; //la barra diminuisce di livello
           if(ProgressBar.fillAmount <= 0.5f) //quando arriva a metà
              ProgressBar.color = Color.red;  //cambia il colore della barra
        }

        
    }
    /* aumenta di poco il livello della barra */
    public void FillProgressBar() {
        ProgressBar.fillAmount += 0.2f;
        /* la barra torna verde se il livello supera la metà */
        if(ProgressBar.fillAmount >= 0.5f) {
          Color myGreen = new Color32(51,212,53,181);
          ProgressBar.color = myGreen;
        }
    }

    public bool IsEmpty() {
        return ProgressBar.fillAmount == 0f;
    }


}
