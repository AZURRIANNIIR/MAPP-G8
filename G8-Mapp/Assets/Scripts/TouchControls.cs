using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

public class TouchControls : MonoBehaviour
{

    [SerializeField] private float movementLength = 100f;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask snakeMask;
    [SerializeField] private LayerMask gridspaceMask;
    [SerializeField] private LayerMask buttonMask;

    private bool snakeCaught = false;

    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        width = (float)Screen.width;
        height = (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(0) && RaycastIsHittingSnake(mousePos))
        {
            snakeCaught = true;
            //print("mus ner");
        }

        if (Input.GetMouseButtonUp(0))
        {
            snakeCaught = false;
        }

        if (snakeCaught)
        {
            RaycastHit2D gridspace = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, gridspaceMask);
            if (gridspace.collider != null)
            {
                print("gridspace träffad");
                GameObject gridHit = gridspace.transform.gameObject;
                BoxCollider2D _bc = gridHit.GetComponent<BoxCollider2D>();
                _bc.enabled = false;
                moveSnake(getSnakeDirection(mousePos), mousePos);
            }

            //transform.position = Vector2.Lerp(transform.position, mousePos, 1000f);
        }

        //if (ButtonHit(mousePos))
        //{
        //    SwitchGrid();
        //}

    }

    private bool RaycastIsHittingSnake(Vector3 mousePos)
    {
        RaycastHit2D orm = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, snakeMask);
        if (orm.collider != null)
        {
            //print("orm träffad");
            return true;
        }
        return false;
    }

    private string getSnakeDirection(Vector3 mousePos)
    {
        RaycastHit2D right = Physics2D.Raycast(mousePos, Vector2.right, 2f, snakeMask);
        if (right.collider != null)
        {
            print("Right");
            return "Right";
        }

        RaycastHit2D left = Physics2D.Raycast(mousePos, Vector2.left, 2f, snakeMask);
        if (left.collider != null)
        {
            print("Left");
            return "Left";
        }

        RaycastHit2D up = Physics2D.Raycast(mousePos, Vector2.up, 2f, snakeMask);
        if (up.collider != null)
        {
            print("Up");
            return "Up";
        }

        RaycastHit2D down = Physics2D.Raycast(mousePos, Vector2.down, 2f, snakeMask);
        if (down.collider != null)
        {
            print("Down");
            return "Down";
        }

        return "Out of bounds";
    }

    private void moveSnake(string direction, Vector2 mousePos)
    {
        Vector2 newPosition = transform.position;
        //newPosition.x = transform.position.x - movementLength;

        if (direction.Equals("Right"))
        {
            newPosition.x = newPosition.x - movementLength;
            transform.position = Vector2.Lerp(transform.position, newPosition, 1000f);
            print("orm flyttad till vänster");
        }

        if (direction.Equals("Left"))
        {
            newPosition.x = newPosition.x + movementLength;
            transform.position = Vector2.Lerp(transform.position, newPosition, 1000f);

        }

        if (direction.Equals("Up"))
        {
            newPosition.y = newPosition.y - movementLength;
            transform.position = Vector2.Lerp(transform.position, newPosition, 1000f);

        }

        if (direction.Equals("Down"))
        {
            newPosition.y = newPosition.y + movementLength;
            transform.position = Vector2.Lerp(transform.position, newPosition, 1000f);

        }

        //transform.position = Vector2.Lerp(transform.position, mousePos, 1000f);

        //transform.position = Vector2.Lerp(transform.position, newPosition, 1000f);
        print("Orm flyttad");

    }

    //bool ButtonHit(Vector2 mousePos)
    //{
    //    RaycastHit2D button = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, buttonMask);
    //    if (button.collider != null)
    //    {
    //        print("knapp träffad");
    //        button.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //        return true;
    //    }
    //    return false;
    //}

    //void SwitchGrid()
    //{
    //    print("Grid switched");
    //}

    //private bool RaycastIsHittingGridspace(Vector3 mousePos)
    //{
    //    RaycastHit2D gridspace = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, gridspaceMask);
    //    if (gridspace.collider != null)
    //    {
    //        print("gridspace träffad");
    //        return true;
    //    }
    //    return false;
    //}

    void FixedUpdate()
    {


    }

}
