using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private const int LMB_NUMBER = 0;
    private float gridSize = 0f;

    [Header("Attributes")]
    [SerializeField] private float movementLength;
    [SerializeField] private float maxAllowedDistanceFromMouse = 1f;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask noMask;
    [Header("Components")]
	[SerializeField] private GameController gameController;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    [SerializeField] private GridList gridListScript;
    [SerializeField] private GameObject startPosition;
    [Header("States")]
    [SerializeField] private bool onTile;

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 currentPosition;
    private Vector3 currentScreenPoint;

    private bool mouseDown;

    private void Awake()
    {
        transform.position = startPosition.transform.position;
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
        if (OnDisabledTile() || IsMouseDistanceTooLong() || OnTileAndTryingToGetOut())
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
        mouseDown = Input.GetMouseButton(LMB_NUMBER);
        //Om "Undo"-funktionen körs så återställs ormen till den förra tilen automatiskt här
        if (Input.GetMouseButtonUp(LMB_NUMBER))
        {
            if (!ClearButton.EventFired)
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
    }
    #region Funktioner som återställer ormen
    private void ResetSnakeToStart()
    {
        transform.position = startPosition.transform.position;
        ResetTrailRenderer();
        gameController.ResetTilesOnGrid();
    }

    private void ResetSnakeToGrid(Transform gridLocation)
    {
        transform.position = gridLocation.position;
        ResetTrailRenderer(); 
    }
    #endregion

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool OnDisabledTile()
    {
        Vector3 mousePos = GetMousePosition();

        RaycastHit2D tile = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, mask);
        if (tile.collider != null)
        {
            //print("orm träffad");
            return true;
        }
        return false;
    }

    private bool IsMouseDistanceTooLong()
    {
        Vector3 mousePos = GetMousePosition();

        if (Vector2.Distance(mousePos, transform.position) > maxAllowedDistanceFromMouse)
        {
            Debug.LogWarning("Avståndet mellan ormen och musen är för långt.");
            return true;
        }
        return false;
    }

    private bool OnTileAndTryingToGetOut()
    {
        Vector3 mousePos = GetMousePosition();

        //Gör en raycast från musens position
        RaycastHit2D outsideTileCheck = Physics2D.Raycast(mousePos, Vector2.zero);
        
        if (onTile)
        {
            if (outsideTileCheck.collider != null)
            {
                if (outsideTileCheck.collider.CompareTag("GridTile") || outsideTileCheck.collider.CompareTag("BridgeTile"))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    #region OnTrigger-funktioner
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GridTile") || collision.CompareTag("BridgeTile"))
        {
            onTile = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GridTile") && collision.CompareTag("BridgeTile"))
        {
            onTile = false;
        }
    }
    #endregion

    private void ResetTrailRenderer()
    {
        snakeTrailRenderer.Clear();
    }

    #region Enable/Disable funktioner
    private void OnEnable()
    {
        ClearButton.OnClick += ResetSnakeToStart;
    }

    private void OnDisable()
    {
        ClearButton.OnClick -= ResetSnakeToStart;
    }
    #endregion
}


