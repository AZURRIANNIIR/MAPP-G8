using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAnimation : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private Image snakeSprite;
    [SerializeField] private Animator panelAnimator;

    private void Update()
    {
        if (gameController.GameWon == true)
        {
            snakeSprite.enabled = true;
            panelAnimator.SetTrigger("StartAnimation");
        }
    }

    private void Start()
    {
        snakeSprite = GetComponent<Image>();
        if (snakeSprite == null)
        {
            Debug.LogError("Image component not found on the same game object as WinAnimation script.");
        }
        else
        {
            snakeSprite.enabled = false;
        }
    }
}
