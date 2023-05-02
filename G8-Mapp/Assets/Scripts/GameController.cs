using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] private ColliderScript[] tileColliders;
    [SerializeField] private TriggerButtonScript button;
    [SerializeField] private GameObject raycastBoxPrefab;
    [SerializeField] private GameObject[] raycastBoxes;
    [SerializeField] private GameObject bridgeBoxPrefab;
    [SerializeField] private GameObject[] bridgeBoxes;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject goal;
    [SerializeField] private int gridTilesLeft;
    private int numberOfTiles;
    public bool win = false;

    private void Start()
    {
        //Lägger till alla tiles i en lista
        raycastBoxes = GameObject.FindGameObjectsWithTag("GridTile");
        bridgeBoxes = GameObject.FindGameObjectsWithTag("BridgeTile");
		
        //sparar antalet tiles kvar att ta vid start (dvs alla)
        numberOfTiles = raycastBoxes.Length + bridgeBoxes.Length;
        gridTilesLeft = numberOfTiles;

        tileColliders = FindObjectsOfType<ColliderScript>();

        //Är ingen orm tilldelad i inspektorn så försöker vi hitta den
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Snake");
        }
    }

    //Kollar om villkoren för vinst är uppfyllda
    private void Update()
    {
        if(gridTilesLeft == 0 && player.transform.position == goal.transform.position)
        {
            win = true;
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
	
    public void ResetTilesOnGrid()
    {
        foreach(ColliderScript colliderScript in tileColliders)
        {
            colliderScript.ResetTile();
        }

        if (button != null)
        {
            button.resetTiles();
        }

        resetNumberOfTilesLeft();
    }

    private void resetNumberOfTilesLeft()
    {
        gridTilesLeft = numberOfTiles;
    }

    #region Enable/Disable funktioner
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
