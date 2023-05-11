using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridList : MonoBehaviour
{
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();
    [SerializeField] SnakeMovement snakeMovement;

    private void Awake()
    {
        if (!snakeMovement)
        {
            snakeMovement = FindObjectOfType<SnakeMovement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!UndoButton.EventFired) 
        {
            if (collision.gameObject.CompareTag("GridTile"))
            {
                if (gridList.Contains(collision.gameObject))
                {
                    return;
                }
                //Om den inte finns i listan
                gridList.Add(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("BridgeTile"))
            {
                BridgeTile bridgeTile = collision.gameObject.GetComponent<BridgeTile>();
                if (!bridgeTile.GetTakenStatus())
                {
                    Debug.Log("L�gger till en bro i listan");
                    gridList.Add(collision.gameObject);
                }
                else
                {
                    Debug.Log("L�gger till en tagen bro i listan");
                    gridList.Add(collision.gameObject);
                }
            }
        }
        
    }

    public GameObject GetMostRecentTile()
    {
        if (gridList.Count == 0)
        {
            return null;
        }
        //Om det finns en grid i listan
        return gridList[gridList.Count - 1];
    }

    public void DeleteTileFromList(GameObject tile)
    {
        if (!gridList.Contains(tile))
        {
            Debug.Log(tile.name + " finns inte i listan.");
            return;
        }

        if (tile.CompareTag("GridTile"))
        {
            gridList.Remove(tile);
        }

        Debug.Log("Nu ska " + tile.name + " tas bort fr�n listan.");
        if (tile.CompareTag("BridgeTile"))
        {
            if (tile.GetComponent<BridgeTile>().GetTakenStatus())
            {
                /* V�nd listans ordning om korsningen �r helt tagen                                            *
                *  Detta f�rhindrar en bug som g�r att ormen annars inte flyttar tillbaka till f�reg�ende ruta */
                gridList.Reverse();
                gridList.Remove(tile);
                gridList.Reverse();
            }
            else
            {
                gridList.Remove(tile);
            }
        }
        
        //�terst�ll Tilens status, annars blir det problem n�r spelaren g�r tillbaka.
        tile.GetComponentInParent<ColliderScript>().ResetTile();
    }

    private void ClearList()
    {
        gridList.Clear();
    }

    public void GridListUndoAction()
    {
        Debug.Log("GridList k�nde av en knapptryckning fr�n undo-knappen");
        DeleteTileFromList(GetMostRecentTile());
    }

    public int GetLength() { return gridList.Count; }

    #region Enable/Disable funktioner
    private void OnEnable()
    {
        UndoButton.OnClick += GridListUndoAction;
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= GridListUndoAction;
    }
    #endregion
}
