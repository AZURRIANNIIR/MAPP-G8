using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerButtonScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileList;

    // Start is called before the first frame update
    void Start()
    {
        resetTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            for (int i = 0; i < tileList.Count; i++)
            {
                tileList[i].GetComponent<ColliderScript>().ResetTile();
            }
            UndoButton.OnClick += resetTiles;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UndoButton.OnClick -= resetTiles;
    }

    public void resetTiles()
    {
        if (tileList.Count == 0) 
        {
            Debug.LogError("Det finns inga tiles i TriggerButtonScripts lista.");
            return;
        }
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].GetComponent<ColliderScript>().disableTile();
        }
    }
}
