using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSound : MonoBehaviour
{
    public AudioSource winSound;
    public GameController gameController;
    void Awake()
    {
        winSound.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GameWon == true)
        {
            winSound.enabled = true;
        }
    }
}
