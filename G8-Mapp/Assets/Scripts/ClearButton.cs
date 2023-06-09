using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    //Denna event kan andra skript subscriba till och k�ra egna funktioner p� n�r den k�rs
    //Det m�ste dock g�ras i kod, inte genom inspektorn som det g�r med UnityEvents
    public static event Action OnClick;

    private Button button;
    private GameController gameController;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        gameController = FindObjectOfType<GameController>();
    }

    public void Update()
    {
        button.interactable = !gameController.GameWon;
    }

    public void ClickAction()
    {
        StartCoroutine(ClearEvent());
        Debug.Log("Spelaren klickade just p� ClearKnappen");
    }

    private IEnumerator ClearEvent()
    {
        yield return new WaitForSeconds(1);
        OnClick?.Invoke();
    }
}
