using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    //Denna event kan andra skript subscriba till och köra egan funktioner
    //Det måste dock göras i kod, inte genom inspektorn som det går med UnityEvents
    [SerializeField] Button undoButton;
    [SerializeField] GridList gridListScript;
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
