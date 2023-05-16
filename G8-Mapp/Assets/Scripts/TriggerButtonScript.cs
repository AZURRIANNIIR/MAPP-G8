using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerButtonScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileList;

    #region Property för listan
    internal List<GameObject> TileList
    {
        get { return tileList; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DisableTilesInList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            for (int i = 0; i < tileList.Count; i++)
            {
                tileList[i].GetComponent<ColliderScript>().ResetTile();
            }
            UndoButton.OnClick += DisableTilesInList;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UndoButton.OnClick -= DisableTilesInList;
    }

    public void DisableTilesInList()
    {
        if (tileList.Count == 0) 
        {
            Debug.LogError("Det finns inga tiles i TriggerButtonScripts lista.");
            return;
        }
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].GetComponent<ColliderScript>().DisableTile();
        }
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= DisableTilesInList;
    }
}
