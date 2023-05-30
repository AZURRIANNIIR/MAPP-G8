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
    [SerializeField] AudioSource clearButtonSound;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        gameController = FindObjectOfType<GameController>();
    }

    public void Update()
    {
        button.interactable = !gameController.GameWon;

        if(PlayerPrefs.GetInt("SOUNDFX_MUTED") == 1)
        {
            clearButtonSound.enabled = false;
        }
    }

    public void ClickAction()
    {
        StartCoroutine(ClearEvent());
    }

    private IEnumerator ClearEvent()
    {
        yield return new WaitForSeconds(1);
        OnClick?.Invoke();
    }
}
