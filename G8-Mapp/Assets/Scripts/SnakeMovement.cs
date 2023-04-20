using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private const int LMB_NUMBER = 0;
    private float gridSize = 0f;

    [SerializeField] private GameController gameController;
    [SerializeField] private float movementLength;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    [SerializeField] private GridList gridListScript;

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 currentPosition;
    private Vector3 currentScreenPoint;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = new Vector3(1f, 1f, 0f);
        transform.position = startPosition;
        snakeTrailRenderer = GetComponent<TrailRenderer>();
        gridListScript = GetComponent<GridList>();
    }

    private void OnMouseDown()
    {
        scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
    }

    private void OnMouseDrag()
    {
        if (OnDisabledTile())
        {
            return;
        }
        //Rörelse längst grid
        currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = new Vector3(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y), Mathf.RoundToInt(currentPosition.z));
        currentPosition.x = (float)(Mathf.RoundToInt(currentPosition.x) + gridSize);
        currentPosition.y = (float)(Mathf.RoundToInt(currentPosition.y) + gridSize); 
        transform.position = currentPosition;  

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(LMB_NUMBER))
        {
            GameObject mostRecentTile = gridListScript.GetMostRecentTile();
            if (mostRecentTile == null)
            {
                ResetSnakeToStart();
            }
            else
            {
                ResetSnakeToGrid(mostRecentTile.transform);
            }
        }
    }

    private void ResetSnakeToStart()
    {
        transform.position = startPosition;
        ResetTrailRenderer();
        gameController.ResetTilesOnGrid();
    }

    private void ResetSnakeToGrid(Transform gridLocation)
    {
        transform.position = gridLocation.position;
        ResetTrailRenderer();
        
    }

    

    private bool OnDisabledTile()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D tile = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, mask);
        if (tile.collider != null)
        {
            //print("orm träffad");
            return true;
        }
        return false;
    }
    private void ResetTrailRenderer()
    {
        snakeTrailRenderer.Clear();
    }
}


