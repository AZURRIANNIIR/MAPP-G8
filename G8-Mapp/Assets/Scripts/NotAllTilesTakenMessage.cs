using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAllTilesTakenMessage : MonoBehaviour
{
    private GameObject holderObject;

    private void Awake()
    {
        holderObject = transform.Find("Panel").gameObject;
        GameController.SnakeOnGoalEarly += ShowObject;
    }

    private void ShowObject() { holderObject.SetActive(true); }

    private void HideObject() { holderObject.SetActive(false); }

    private void OnEnable()
    {
        GameController.SnakeOnGoalEarly += ShowObject;
        UndoButton.OnClick += HideObject;
    }

    private void OnDisable()
    {
        GameController.SnakeOnGoalEarly -= ShowObject;
        UndoButton.OnClick -= HideObject;
    }
}
