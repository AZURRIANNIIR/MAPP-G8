using System.Linq;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private const int LMB_NUMBER = 0;
    private float gridSize = 0f;

    [Header("Attributes")]
    [SerializeField] private float movementLength;
    [SerializeField] private float maxAllowedDistanceFromMouse = 0.7f;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask horizontalBridgeEdge;
    [SerializeField] private LayerMask verticalBridgeEdge;

    [Header("Components")]
	[SerializeField] private GameController gameController;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    [SerializeField] private GridList gridListScript;
    [Header("States")]
    [SerializeField] private bool onTile;
    public bool enteredHorizontally;
    public bool enteredVertically;

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 currentPosition;
    private Vector3 currentScreenPoint;
    private readonly Vector2 startPosition = new Vector3(1f, 1f);

    private bool mouseDown;

    private void Awake()
    {
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

        //följande kod används för att reglera crossroads, den kollar om man rör specifika colliders på insidan av crossroadtiles
        Vector3 mousePos = GetMousePosition();

        RaycastHit2D horizontalEdge = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, horizontalBridgeEdge);
        if (horizontalEdge.collider != null)
        {
            horizontalEdge.collider.enabled = false;
            enteredVertically = true;

        }

        RaycastHit2D verticalEdge = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, verticalBridgeEdge);
        if (verticalEdge.collider != null)
        {
            verticalEdge.collider.enabled = false;
            enteredHorizontally = true;
 
        }
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
        transform.position = startPosition;
        ResetTrailRenderer();
        gameController.ResetTilesOnGrid();
    }

    private void ResetSnakeToGrid(Transform gridLocation)
    {
        transform.position = gridLocation.position;
        
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

        //Skapa en raycast från musens position
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

        if (collision.CompareTag("BridgeTile"))
        {
            enteredHorizontally = false;
            enteredVertically = false;
        }
    }
    #endregion

    private void ResetTrailRenderer()
    {
        snakeTrailRenderer.Clear();
    }

    public void TrailRendererPositions()
    {

        var positions = new Vector3[snakeTrailRenderer.positionCount];
        snakeTrailRenderer.GetPositions(positions);
        var positionsList = positions.ToList();

        positionsList.RemoveAt(snakeTrailRenderer.positionCount - 1);

        snakeTrailRenderer.Clear();
        snakeTrailRenderer.AddPositions(positionsList.ToArray());

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


