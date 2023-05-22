using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Vi ärver här från GridTile-skriptet
public class BridgeTile : GridTile
{
    [SerializeField] public bool crossedOnce;
    [SerializeField] private SnakeMovement snakeMovement;
    [SerializeField] private GridList gridList;

    [Header("Colliders")]
    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    [SerializeField] private GameObject temporaryCollider;

    [Header("States")]
    [SerializeField] private bool steppedOn;

    [SerializeField] private int tileNumber;


    public UnityEvent OnCrossedOnceStatus;

    new private void Start()
    {
        base.Start();
        crossedOnce = false;

        if (!snakeMovement)
        {
            snakeMovement = FindObjectOfType<SnakeMovement>();
        }
        
    }

    private void Update()
    {
/*        if (steppedOn)
        {
            if (snakeMovement.bridgeDisabled)
            {
                temporaryCollider.GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (!snakeMovement.bridgeDisabled)
            {
                temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
                steppedOn = false;
            }
        }*/

        if (steppedOn && gridList.GetLength() == tileNumber + 2)
        {
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = true;
        } else
        {
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!UndoButton.EventFired)
        {
            //Om vi redan har korsat bron en gång
            if (collision.gameObject.CompareTag("Snake") && crossedOnce && !GetTakenStatus())
            {
                SetTakenStatus(true);
                print("ny plats");
                tileCollider.TakeTile();
                gameController.tileTaken();
                //tileNumber = gridList.GetLength();
                OnTakenStatus?.Invoke();
                return;
            }

            //Om bron korsas för första gången
            else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !GetTakenStatus())
            {
                //Typkonvertera för att se till så att vi kan använda korsningsfunktioner
                if (tileCollider.GetType() == typeof(BridgeColliderScript))
                {
                    BridgeColliderScript bridgeCollider = (BridgeColliderScript)tileCollider;
                    bridgeCollider.ChangeBridgeSpriteToTaken();
                }
                crossedOnce = true;
                OnCrossedOnceStatus?.Invoke();
                gameController.tileTaken();
                tileNumber = gridList.GetLength();
            }
        }
    }

    new private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Snake"))
        {
            steppedOn = true;
            turnOnPath();
        }

        if (UndoButton.EventFired && !crossedOnce && !GetTakenStatus())
        {
            steppedOn = false;
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public bool GetCrossedOnceStatus()
    {
        return crossedOnce;
    }

    public void SetCrossedOnceStatus(bool state)
    {
        crossedOnce = state;
    }

    private void turnOnPath()
    {
        print("Colliders borde stängas av");
        upperBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        lowerBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        leftBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        rightBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void turnOffPath(string direction)
    {
        print("Colliders borde sättas på");
        if (direction == "Horizontal"){
            upperBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
            lowerBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (direction == "Vertical")
        {
            leftBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
            rightBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
