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
    [SerializeField] AudioSource buttonSound;
    
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

        if(PlayerPrefs.GetInt("SOUNDFX_MUTED") == 1)
        {
            buttonSound.enabled = false;
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
        
        EventFired = false;
    }
}
