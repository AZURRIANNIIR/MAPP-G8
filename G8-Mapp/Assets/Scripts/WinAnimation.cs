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
        //Om spelaren klarat banan
        if (gameController.GameWon == true)
        {
            //enabla ormen och starta animationen
            snakeSprite.enabled = true;
            panelAnimator.SetTrigger("StartAnimation");
        }
    }

    private void Start()
    {
        snakeSprite = GetComponent<Image>();

        // här nere kollar jag om ormen har image på sig
        if (snakeSprite == null)
        {
            Debug.LogError("Image komponenten hittas inte på samma game object som WinAnimation script.");
        }
        else // om den finns kan disabla den så att den inte syns förens spelaren vunnit
        {
            snakeSprite.enabled = false;
        }
    }
}