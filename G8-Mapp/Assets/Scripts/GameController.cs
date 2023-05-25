using System;
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
    [field:SerializeField] public bool GameWon { get; private set; }

    public static event Action SnakeOnGoalEarly;

    private void Awake()
    {
        if (!button)
        {
            button = FindObjectOfType<TriggerButtonScript>();
        }
        if (!goal)
        {
            goal = GameObject.FindGameObjectWithTag("Goal");
        }
    }

    [System.Obsolete]
    private void Start()
    {
        
        //Lägger till alla tiles i en lista
        raycastBoxes = GameObject.FindGameObjectsWithTag("GridTile");
        bridgeBoxes = GameObject.FindGameObjectsWithTag("BridgeTile");
		
        //sparar antalet tiles kvar att ta vid start (dvs alla)
        numberOfTiles = raycastBoxes.Length + bridgeBoxes.Length;
        gridTilesLeft = raycastBoxes.Length + (bridgeBoxes.Length * 2);

        tileColliders = FindObjectsOfType<ColliderScript>();

        //Är ingen orm tilldelad i inspektorn så försöker vi hitta den
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Snake");
        }
    }

    //Kollar om villkoren för vinst är uppfyllda
    [System.Obsolete]
    private void Update()
    {
        Win();
        //Om checken är sätt till 0, så buggar meddelandet ut och visas även när spelaren har vunnit nivån.
        if (player.transform.position == goal.transform.position && gridTilesLeft > 1)
        {
            Debug.Log("Spelaren gick till målet utan att ha tagit alla tiles");
            SnakeOnGoalEarly?.Invoke();
        }
    }

    public void Win()
    {
        if (gridTilesLeft == 0 && player.transform.position == goal.transform.position)
        {
            GameWon = true;
        }
    }

    public void TileTaken()
    {
        gridTilesLeft--;
    }

    //Metodnamn upp för debatt
    public void TileNotTaken()
    {
        gridTilesLeft++;
    }
	
    public void ResetTilesOnGrid()
    {
        foreach(ColliderScript colliderScript in tileColliders)
        {
            //Knapppen får själv ta hand om de tiles som finns i dess lista
            if (!button.TileList.Contains(colliderScript.gameObject))
            {
                colliderScript.ResetTile();
            }
        }

        if (button != null)
        {
            button.DisableTilesInList();
        }

        if (!UndoButton.EventFired) 
        {
            ResetNumberOfTilesLeft();
        }
        
    }

    private void ResetNumberOfTilesLeft()
    {
        gridTilesLeft = numberOfTiles;
    }

    #region Enable/Disable funktioner
    private void OnEnable()
    {
        UndoButton.OnClick += TileNotTaken;
        SnakeMovement.OnReturnToStart += ResetTilesOnGrid;
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= TileNotTaken;
        SnakeMovement.OnReturnToStart -= ResetTilesOnGrid;
    }    
    #endregion

}
