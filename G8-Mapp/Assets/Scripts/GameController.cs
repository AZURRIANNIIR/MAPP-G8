using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject raycastBoxPrefab;
    [SerializeField] private GameObject[] raycastBoxes;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject goal;
    [SerializeField] private int gridTilesLeft;

    private void Start()
    {
        //Lägger till alla tiles i en lista
        raycastBoxes = GameObject.FindGameObjectsWithTag("GridTile");
		
        //sparar antalet tiles kvar att ta vid start (dvs alla)
        gridTilesLeft = raycastBoxes.Length;
    }

    //Kollar om villkoren för vinst är uppfyllda
    private void Update()
    {
        if(gridTilesLeft == 0 && player.transform.position == goal.transform.position)
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

    #region Enable/Disable functions
    private void OnEnable()
    {
        UndoButton.OnClick += tileNotTaken;
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= tileNotTaken;
    }
    #endregion


}
