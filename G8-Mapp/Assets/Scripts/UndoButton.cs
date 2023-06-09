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
    [SerializeField] GameObject startPosition;
    
    private GameController gameController;

    public static bool EventFired { get; private set; }

    //Denna event kan andra skript subscriba till och k�ra egna funktioner
    //Det m�ste dock g�ras i kod, inte genom inspektorn som det g�r med UnityEvents
    public static event Action OnClick;

    private void Awake()
    {
        undoButton = GetComponentInChildren<Button>();
        gridListScript = FindObjectOfType<GridList>();
        gameController = FindObjectOfType<GameController>();
        if (!startPosition)
        {
            startPosition = GameObject.FindGameObjectWithTag("StartPosition");
        }
    }

    private void Update()
    {
        if (!gridListScript.IsListEmpty() && (gridListScript.GetMostRecentTile().transform.position == startPosition.transform.position))
        {
            undoButton.interactable = false;
        }
        else
        {
            undoButton.interactable = !gridListScript.IsListEmpty() && !gameController.GameWon;
        }
    }

    public void ClickAction()
    {
        OnClick?.Invoke();
        EventFired = true;
        Invoke(nameof(SetEventStatusToFalse), INVOKE_DELAY);
    }

    private void SetEventStatusToFalse()
    {
        Debug.Log("Nu �r inte eventen p� UndoButton l�ngre aktiv");
        EventFired = false;
    }
}
