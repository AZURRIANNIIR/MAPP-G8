using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private const float INVOKE_DELAY = 0.035f;

    [SerializeField] Button undoButton;
    [SerializeField] GridList gridListScript;
    private GameController gameController;

    public static bool EventFired { get; private set; }

    //Denna event kan andra skript subscriba till och köra egna funktioner
    //Det måste dock göras i kod, inte genom inspektorn som det går med UnityEvents
    public static event Action OnClick;

    private void Awake()
    {
        undoButton = GetComponentInChildren<Button>();
        gridListScript = FindObjectOfType<GridList>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        undoButton.interactable = !gridListScript.IsListEmpty() && !gameController.GameWon;
    }

    public void ClickAction()
    {
        OnClick?.Invoke();
        EventFired = true;
        Invoke("SetEventStatusToFalse", INVOKE_DELAY);
    }

    private void SetEventStatusToFalse()
    {
        Debug.Log("Nu är inte eventen på UndoButton längre aktiv");
        EventFired = false;
    }
}
