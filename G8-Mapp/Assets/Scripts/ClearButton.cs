using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ClearButton : MonoBehaviour
{
    private const float INVOKE_DELAY = 0.02f;
    
    public static bool EventFired { get; private set; }

    //Denna event kan andra skript subscriba till och k�ra egna funktioner p� n�r den k�rs
    //Det m�ste dock g�ras i kod, inte genom inspektorn som det g�r med UnityEvents
    public static event Action OnClick;

    public void ClickAction()
    {
        StartCoroutine(ClearEvent());
        Debug.Log("Spelaren klickade just p� ClearKnappen");
        EventFired = true;
        //K�r nedanst�ende funktion en kort tid efter att vi k�rt v�ran Event
        Invoke("SetEventStatusToFalse", INVOKE_DELAY);
    }

    private void SetEventStatusToFalse()
    {
        EventFired = false;
    }

    private IEnumerator ClearEvent()
    {
        yield return new WaitForSeconds(1);
        OnClick?.Invoke();
    }
}
