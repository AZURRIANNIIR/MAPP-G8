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
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !taken)
        {
            taken = true;
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
            return;
        }
        else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !taken)
        {
            tileCollider.ChangeBridgeColor();
            crossedOnce = true;
            print("bridge taken once");
        }
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }
}

