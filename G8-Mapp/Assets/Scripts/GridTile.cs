using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private bool taken = false;
    [SerializeField] private ColliderScript tileCollider;
    [SerializeField] private GameController gameController;


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && !taken)
        {
            taken = true;
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && taken)
        {
            tileCollider.enableCollider();
        }
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }
}
