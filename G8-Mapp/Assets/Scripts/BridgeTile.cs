using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Vi ärver här från GridTile-skriptet
public class BridgeTile : GridTile
{
    [SerializeField] public bool crossedOnce;
    [SerializeField] private SnakeMovement snakeMovement;

    [Header("Triggers")]
    //[SerializeField] private GameObject upperTriggerCollider;
    //[SerializeField] private GameObject lowerTriggerCollider;
    //[SerializeField] private GameObject leftTriggerCollider;
    //[SerializeField] private GameObject rightTriggerCollider;

    [Header("Colliders")]
    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    [SerializeField] private GameObject temporaryCollider;

    [Header("States")]
    [SerializeField] private bool steppedOn;

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
        //if (!snakeMovement.enteredHorizontally)
        //{
        //    turnOnPath(upperBoxCollider, lowerBoxCollider); /*, upperTriggerCollider, lowerTriggerCollider);*/
        //}

        //if (!snakeMovement.enteredVertically)
        //{
        //    turnOnPath(leftBoxCollider, rightBoxCollider); /*, leftTriggerCollider, rightTriggerCollider);*/
        //}

        if (steppedOn)
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
        }

        ////om ormen kommer från höger eller vänster
        //if (snakeMovement.enteredHorizontally)
        //{
        //    print("riktning läses av");
        //    turnOffPath("Horizontal"); /*, upperTriggerCollider, lowerTriggerCollider);*/
        //}

        ////om ormen kommer upp eller ner ifrån
        //else if (snakeMovement.enteredVertically)
        //{
        //    print("riktning läses av");
        //    turnOffPath("Vertical"); /*, leftTriggerCollider, rightTriggerCollider);*/
        //}

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
                print("bridge taken once");
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

        if (UndoButton.EventFired)
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

    private void turnOnPath()/*GameObject colliderA, GameObject colliderB, GameObject triggerA, GameObject triggerB)*/
    {
        print("Colliders borde stängas av");
        upperBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        lowerBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        leftBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        rightBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        //triggerA.GetComponent<BoxCollider2D>().enabled = true;
        //triggerB.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void turnOffPath(string direction)/*GameObject colliderA, GameObject colliderB, GameObject triggerA, GameObject triggerB)*/
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

        //triggerA.GetComponent<BoxCollider2D>().enabled = false;
        //triggerB.GetComponent<BoxCollider2D>().enabled = false;
    }
}
