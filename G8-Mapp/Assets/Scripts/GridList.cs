using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridList : MonoBehaviour
{
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
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
            Debug.Log("Vi gick �ver en bro");
            if (collision.TryGetComponent(out BridgeTile bridgeScript))
            {
                if (!bridgeScript.GetTakenStatus())
                {
                    Debug.Log("Bron har endast korsats en g�ng");
                }
            }
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

        Debug.Log("Nu ska " + tile.name + " tas bort fr�n listan.");
        //�terst�ll Tilens status, annars blir det problem n�r spelaren g�r tillbaka.
        tile.GetComponentInParent<ColliderScript>().ResetTile();
        gridList.Remove(tile);
    }

    private void ClearList()
    {
        gridList.Clear();
    }

    public void GridListUndoAction()
    {
        Debug.Log("GridList k�nde av en knapptryckning fr�n undo-knappen");
        DeleteTileFromList(gridList[gridList.Count - 1]);
    }

    public int GetLength() { return gridList.Count; }

    #region Enable/Disable funktioner
    private void OnEnable()
    {
        UndoButton.OnClick += GridListUndoAction;
        ClearButton.OnClick += ClearList;
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= GridListUndoAction;
        ClearButton.OnClick -= ClearList;
    }
    #endregion
}
