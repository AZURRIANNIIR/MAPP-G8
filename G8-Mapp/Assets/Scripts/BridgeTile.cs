using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTile : MonoBehaviour
{

    public bool crossedOnce = false;
    public bool taken = false;
    [SerializeField] private ColliderScript tileCollider;
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !taken)
        {
            taken = true;
            print("ny plats");
            //tileCollider.TakeTile();
            gameController.tileTaken();
        }

        crossedOnce = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            print(taken);
        }
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }
}

