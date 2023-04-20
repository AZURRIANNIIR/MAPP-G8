using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    private const float INVOKE_DELAY = 0.01f;
    //Denna event kan andra skript subscriba till och köra egna funktioner
    //Det måste dock göras i kod, inte genom inspektorn som det går med UnityEvents
    public static event Action OnClick;

    public static bool EventFired {  get; private set; }

    public void ClickAction()
    {
        OnClick.Invoke();
        Debug.Log("Spelaren klickade just på ClearKnappen");
        EventFired = true;
        //Kär nedanstående funktion en kort tid efter att vi kört våran Event
        Invoke("SetEventStatusToFalse", INVOKE_DELAY);
    }

    private void SetEventStatusToFalse()
    {
        EventFired = false;
    }
}
