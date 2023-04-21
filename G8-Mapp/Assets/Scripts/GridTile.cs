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
        if (!tileCollider)
        {
            tileCollider = GetComponentInParent<ColliderScript>();
        }
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
            tileCollider.EnableCollider();
        }
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }

    //Kodrepetition, men den ovanstående fungerar inte med ClearButtons event
    private void SetTakenStatusToFalse() => SetTakenStatus(false);

    #region Enable/Disable funktioner
    private void OnEnable()
    {
        ClearButton.OnClick += SetTakenStatusToFalse;
    }

    private void OnDisable()
    {
        ClearButton.OnClick += SetTakenStatusToFalse;
    }
    #endregion
}
