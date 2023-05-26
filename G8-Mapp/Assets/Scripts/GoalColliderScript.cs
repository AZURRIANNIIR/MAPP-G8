using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalColliderScript : MonoBehaviour
{
    GameController gameController;
    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getTilesLeft() == 1)
        {
            collider.enabled = false;
        } else
        {
            collider.enabled = true;
        }
    }
}
