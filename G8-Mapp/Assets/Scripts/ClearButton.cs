using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ClearButton : MonoBehaviour
{
    private const float INVOKE_DELAY = 0.02f;
    
    public static bool EventFired { get; private set; }

    //Denna event kan andra skript subscriba till och köra egna funktioner på när den körs
    //Det måste dock göras i kod, inte genom inspektorn som det går med UnityEvents
    public static event Action OnClick;

    public void ClickAction()
    {
        StartCoroutine(ClearEvent());
        Debug.Log("Spelaren klickade just på ClearKnappen");
        EventFired = true;
        //Kör nedanstående funktion en kort tid efter att vi kört våran Event
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
