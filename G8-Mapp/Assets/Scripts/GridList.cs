using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridList : MonoBehaviour
{
    [SerializeField] private bool findAllTilesOnLaunch;
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();

    private void Awake()
    {
        if (findAllTilesOnLaunch)
        {
            GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Grid");
            foreach (GameObject tile in allTiles)
            {
                gridList.Add(tile);
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gridList.Contains(collision.gameObject) && collision.gameObject.CompareTag("Grid"))
        {
            Debug.Log("Lägger till grid i listan");
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
}
