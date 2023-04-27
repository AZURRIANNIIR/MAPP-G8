using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] private bool taken = false;
    [SerializeField] protected ColliderScript tileCollider;
    [SerializeField] protected GameController gameController;


    protected void Start()
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

    protected void OnTriggerExit2D(Collider2D collision)
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

    public bool GetTakenStatus() { return taken; }

    //Kodrepetition, men den ovanst�ende motsvarigheten fungerar inte med ClearButtons event
    private void SetTakenStatusToFalse() => SetTakenStatus(false);

    #region Enable/Disable funktioner
    protected void OnEnable()
    {
        ClearButton.OnClick += SetTakenStatusToFalse;
    }

    protected void OnDisable()
    {
        ClearButton.OnClick += SetTakenStatusToFalse;
    }
    #endregion
}
