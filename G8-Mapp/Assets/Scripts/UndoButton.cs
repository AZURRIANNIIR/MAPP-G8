using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] Button undoButton;
    [SerializeField] GridList gridListScript;

    //Denna event kan andra skript subscriba till och k�ra egna funktioner
    //Det m�ste dock g�ras i kod, inte genom inspektorn som det g�r med UnityEvents
    public static event Action OnClick;

    private void Awake()
    {
        undoButton = GetComponentInChildren<Button>();
        gridListScript = FindObjectOfType<GridList>();
    }


    private void Update()
    {
        undoButton.interactable = gridListScript.GetLength() > 0;
    }

    public void ClickAction()
    {
        OnClick?.Invoke();
    }
}
