using System.Linq;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private const int LMB_NUMBER = 0;
    private const int DIRECTION_ANGLE = 90;
    private float gridSize = 0f;

    [Header("Attributes")]
    [SerializeField] private float movementLength;
    [SerializeField] private float maxAllowedDistanceFromMouse = 0.7f;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask horizontalBridgeEdge;
    [SerializeField] private LayerMask verticalBridgeEdge;
    [SerializeField] private Transform startPosition;
    [Header("Components")]
    [SerializeField] private GameController gameController;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    [SerializeField] private GridList gridListScript;
    [Header("States")]
    [SerializeField] private bool onTile;
    public bool enteredHorizontally;
    public bool enteredVertically;
    public string lastEnterDirection;
    public bool bridgeDisabled;
    public BridgeTile lastBridgeTile;

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 currentPosition;
    private Vector3 currentScreenPoint;

    private void Awake()
    {
        if (!startPosition)
        {
            startPosition = GameObject.FindGameObjectWithTag("StartPosition").transform;
        }
        transform.position = startPosition.position;
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
        if (OnDisabledTile() || IsMouseDistanceTooLong() || OnTileAndTryingToGetOut() || !IsDirectionMultipleOfDirectionAngle())
        {
            return;
        }

        //Rörelse längst grid
        currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);

        //Tog bort denna för att diagonal rörelse skulle funka, men vågar inte radera kodraden
        transform.position = new Vector3(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y), Mathf.RoundToInt(currentPosition.z));

        currentPosition.x = (float)(Mathf.RoundToInt(currentPosition.x) + gridSize);
        currentPosition.y = (float)(Mathf.RoundToInt(currentPosition.y) + gridSize);

        //Ser till att diagonal rörelse inte sker
        /*if (transform.position.x == currentPosition.x || transform.position.y == currentPosition.y)
        {
            transform.position = currentPosition;
        }*/

        //följande kod används för att reglera crossroads, den kollar om man rör specifika colliders på insidan av crossroadtiles
        Vector3 mousePos = GetMousePosition();

        RaycastHit2D horizontalEdge = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, horizontalBridgeEdge);
        if (horizontalEdge.collider != null)
        {
            horizontalEdge.collider.enabled = false;
            enteredVertically = true;
            lastEnterDirection = "enteredVertically";

        }

        RaycastHit2D verticalEdge = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, verticalBridgeEdge);
        if (verticalEdge.collider != null)
        {
            verticalEdge.collider.enabled = false;
            enteredHorizontally = true;
            lastEnterDirection = "enteredHorizontally";

        }
    }

    private void Update()
    {
        //Om "Undo"-funktionen körs så återställs ormen till den förra tilen automatiskt här
        if (Input.GetMouseButtonUp(LMB_NUMBER))
        {
            if (!ClearButton.EventFired)
            {
                GameObject mostRecentTile = gridListScript.GetMostRecentTile();
                switch (mostRecentTile)
                {
                    case null: ResetSnakeToStart(); break;
                    default: ResetSnakeToGrid(mostRecentTile.transform); break;
                }
            }
        }

        if (bridgeDisabled)
        {
            UndoButton.OnClick += setLastBridgeEnterDirection;
            UndoButton.OnClick += setBridgeNotTaken;
        }
        else
        {
            UndoButton.OnClick -= setLastBridgeEnterDirection;
            UndoButton.OnClick -= setBridgeNotTaken;

        }
    }

    public void setLastBridgeEnterDirection()
    {
        if (lastEnterDirection.Equals("enteredVertically"))
        {
            enteredVertically = true;
        }

        else if (lastEnterDirection.Equals("enteredHorizontally"))
        {
            enteredHorizontally = true;
        }
    }

    public void setBridgeNotTaken()
    {
        lastBridgeTile.crossedOnce = false;
    }

    #region Funktioner som återställer ormen
    private void ResetSnakeToStart()
    {
        transform.position = startPosition.position;
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
    #region Funktioner som hindrar ormen från att gå om de är true (eller falsk för den sista)
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

    private bool IsDirectionMultipleOfDirectionAngle()
    {
        Vector3 mousePos = GetMousePosition();

        Ray2D mouseRay = new Ray2D(transform.position, (mousePos - transform.position).normalized);

        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.blue);
       
        //Är riktningen ett nummer som är en multiplicering av våran riktningskonstant? (det vill säga en rät linje)
        if (Mathf.RoundToInt(mouseRay.direction.x) % DIRECTION_ANGLE == 0 || Mathf.RoundToInt(mouseRay.direction.y) % DIRECTION_ANGLE == 0)
        {
            return true;
        }
        //Om inte, så kan inte ormen röra sig
        return false;
    }
    #endregion
    #region OnTrigger-funktioner
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GridTile") || collision.CompareTag("BridgeTile"))
        {
            onTile = true;
        }

        if (collision.CompareTag("GridTile"))
        {
            enteredHorizontally = false;
            enteredVertically = false;
            //bridgeDisabled = true;
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
            //enteredHorizontally = false;
            //enteredVertically = false;
            bridgeDisabled = true;
            lastBridgeTile = collision.GetComponent<BridgeTile>();

        }

        if (collision.CompareTag("GridTile"))
        {
            bridgeDisabled = false;
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
        UndoButton.OnClick += TrailRendererPositions;
    }

    private void OnDisable()
    {
        UndoButton.OnClick -= TrailRendererPositions;
    }
    #endregion

}


