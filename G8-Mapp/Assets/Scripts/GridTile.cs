using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridTile : MonoBehaviour
{
    [SerializeField] private bool taken = false;
    [SerializeField] protected ColliderScript tileCollider;
    [SerializeField] protected GameController gameController;
    public UnityEvent OnTakenStatus;

    protected void Start()
    {
        if (!tileCollider)
        {
            tileCollider = GetComponentInParent<ColliderScript>();
        }

        if (!gameController)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && !taken && !UndoButton.EventFired)
        {
            SetTileAsTaken();
        }
    }

    private void SetTileAsTaken()
    {
        taken = true;
        print("ny plats");
        tileCollider.TakeTile();
        gameController.tileTaken();
        OnTakenStatus.Invoke();
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
}
