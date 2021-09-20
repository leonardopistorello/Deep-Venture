using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIBomb : MonoBehaviour, IPointerDownHandler
{
    // Non so se questo codice funziona con i Touch, dobbiamo verificarlo...
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameController.AreBombsAvailable())
            GameController.DetonateBomb();
    }
}
