using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridList : MonoBehaviour
{
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();

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
        return gridList[gridList.Count - 1];
    }
}
