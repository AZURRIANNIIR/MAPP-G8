using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTile : MonoBehaviour
{

    public bool crossedOnce;
    public bool taken = false;
    [SerializeField] private ColliderScript tileCollider;
    [SerializeField] private GameController gameController;

    private void Start()
    {
        crossedOnce = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Om vi redan har korsat bron en gång
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !taken)
        {
            taken = true;
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
            return;
        }

        //Om bron korsas för första gången
        else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !taken)
        {
            tileCollider.ChangeBridgeColor();
            crossedOnce = true;
            print("bridge taken once");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && taken)
        {
            tileCollider.EnableCollider();
        }
    }

    public bool GetCrossedOnce()
    {
        return crossedOnce;
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }

    public void SetCrossedOnceStatus(bool state)
    {
        crossedOnce = state;
    }
}

