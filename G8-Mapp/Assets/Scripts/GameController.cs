using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject raycastBoxPrefab;
    public GameObject[] raycastBoxes;
    public int gridTilesLeft;

    private void Start()
    {
        //Lägger till alla tiles i en lista
        raycastBoxes = GameObject.FindGameObjectsWithTag("GridTile");

        foreach(GameObject GridTile in raycastBoxes)
        {
            Instantiate(raycastBoxPrefab, GridTile.transform.position, GridTile.transform.rotation);
        }

        //sparar antalet vid start
        gridTilesLeft = raycastBoxes.Length;
    }

    private void Update()
    {
        if(gridTilesLeft == 0)
        {
            print("win");
        }
    }

    public void tileTaken()
    {
        gridTilesLeft = gridTilesLeft- 1;
        print("Tagen");
    }

    //Metodnamn upp för debatt
    public void tileNotTaken()
    {
        gridTilesLeft += 1;
    }
}
