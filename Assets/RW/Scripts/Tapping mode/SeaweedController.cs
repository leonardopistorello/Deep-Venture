using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedController : MonoBehaviour
{
    ProgressBarController bar;
   void Start() {
        bar = ProgressBarController.Instance;
   }
    void OnMouseDown() {
        bar.FillProgressBar();
        Kill();
    }

    private void Kill() {
        AudioSource grass = this.gameObject.GetComponent<AudioSource>();
        grass.Play();
        this.gameObject.SetActive(false);
       ObjectPool.SharedInstance.disabledSeaweeds.Add(this.gameObject);
    }
}
