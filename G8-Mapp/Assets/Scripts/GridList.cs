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
                gridList.Add(collision.gameObject);
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

        Debug.Log("Nu ska " + tile.name + " tas bort från listan.");
        gridList.Remove(tile);
        //Återställ Tilens status, annars blir det problem när spelaren går tillbaka.
        tile.GetComponentInParent<ColliderScript>().ResetTile();
    }

    private void ClearList()
    {
        gridList.Clear();
    }

    public void GridListUndoAction()
    {
        Debug.Log("GridList kände av en knapptryckning från undo-knappen");
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
