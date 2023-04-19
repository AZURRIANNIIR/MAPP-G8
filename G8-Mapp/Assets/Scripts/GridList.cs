using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridList : MonoBehaviour
{
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gridList.Contains(collision.gameObject) && collision.gameObject.CompareTag("GridTile"))
        {
            Debug.Log("Lägger till " + collision.gameObject.name + " i listan");
            gridList.Add(collision.gameObject);
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
        //Återställ Tilens status, annars blir det problem när spelaren går tillbaka.
        tile.GetComponent<GridTile>().SetTakenStatus(false);
        gridList.Remove(tile);
    }

    public void GridListUndoAction()
    {
        Debug.Log("GridList kände av en knapptryckning från undo-knappen");
        DeleteTileFromList(gridList[gridList.Count - 1]);
    }

    public int GetLength()
    {
        return gridList.Count;
    }

    #region Enable/Disable functions

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
