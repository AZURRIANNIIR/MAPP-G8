using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    private const float INVOKE_DELAY = 0.01f;
    //Denna event kan andra skript subscriba till och k�ra egna funktioner
    //Det m�ste dock g�ras i kod, inte genom inspektorn som det g�r med UnityEvents
    public static event Action OnClick;

    public static bool EventFired {  get; private set; }

    public void ClickAction()
    {
        OnClick.Invoke();
        Debug.Log("Spelaren klickade just p� ClearKnappen");
        EventFired = true;
        //K�r nedanst�ende funktion en kort tid efter att vi k�rt v�ran Event
        Invoke("SetEventStatusToFalse", INVOKE_DELAY);
    }

    private void SetEventStatusToFalse()
    {
        EventFired = false;
    }
}
