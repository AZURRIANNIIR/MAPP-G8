using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vi ärver här från GridTile-skriptet
public class BridgeTile : GridTile
{
    [SerializeField] private bool crossedOnce;
    [SerializeField] private SnakeMovement snakeMovement;

    [SerializeField] private GameObject upperTriggerCollider;
    [SerializeField] private GameObject lowerTriggerCollider;
    [SerializeField] private GameObject leftTriggerCollider;
    [SerializeField] private GameObject rightTriggerCollider;

    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    [SerializeField] private GameObject temporaryCollider;

    private bool collidersEnabled;
    [SerializeField] private bool steppedOn;

    new private void Start()
    {
        base.Start();
        crossedOnce = false;
    }

    private void Update()
    {
        if (!snakeMovement.enteredHorizontally)
        {
            turnOnPath(upperBoxCollider, lowerBoxCollider, upperTriggerCollider, lowerTriggerCollider);
        }

        if (!snakeMovement.enteredVertically)
        {
            turnOnPath(leftBoxCollider, rightBoxCollider, leftTriggerCollider, rightTriggerCollider);
        }

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
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Om vi redan har korsat bron en gång
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !GetTakenStatus())
        {
            SetTakenStatus(true);
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
            return;
        }

        //Om bron korsas för första gången
        else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !GetTakenStatus())
        {
            //om ormen kommer från höger eller vänster
            if (snakeMovement.enteredHorizontally)
            {
                turnOffPath(upperBoxCollider, lowerBoxCollider, upperTriggerCollider, lowerTriggerCollider);
            }

            //om ormen kommer upp eller ner ifrån
            else if (snakeMovement.enteredVertically)
            {
                turnOffPath(leftBoxCollider, rightBoxCollider, leftTriggerCollider, rightTriggerCollider);

            }

            tileCollider.ChangeBridgeColor();
            crossedOnce = true;
            print("bridge taken once");
        }
    }

    new private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            steppedOn = true;
        }

        //turnOnPath(leftBoxCollider, rightBoxCollider, leftTriggerCollider, rightTriggerCollider);
        //turnOnPath(upperBoxCollider, lowerBoxCollider, upperTriggerCollider, lowerTriggerCollider);

    }

    public bool GetCrossedOnceStatus()
    {
        return crossedOnce;
    }

    public void SetCrossedOnceStatus(bool state)
    {
        crossedOnce = state;
    }

    //Samma problem som för SetTakenStatus i parent-skriptet, grundimplementationen fungerar inte med våran Event från Clear-Button
    private void SetCrossedOnceStatusToFalse() => SetCrossedOnceStatus(false);

    #region Enable/Disable-funktioner
    new private void OnEnable()
    {
        base.OnEnable();
        ClearButton.OnClick += SetCrossedOnceStatusToFalse;
    }

    new private void OnDisable()
    {
        base.OnDisable();
        ClearButton.OnClick -= SetCrossedOnceStatusToFalse;
    }
    #endregion

    private void turnOnPath(GameObject colliderA, GameObject colliderB, GameObject triggerA, GameObject triggerB)
    {
        colliderA.GetComponent<BoxCollider2D>().enabled = false;
        colliderB.GetComponent<BoxCollider2D>().enabled = false;
        triggerA.GetComponent<BoxCollider2D>().enabled = true;
        triggerB.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void turnOffPath(GameObject colliderA, GameObject colliderB, GameObject triggerA, GameObject triggerB)
    {
        colliderA.GetComponent<BoxCollider2D>().enabled = true;
        colliderB.GetComponent<BoxCollider2D>().enabled = true;
        triggerA.GetComponent<BoxCollider2D>().enabled = false;
        triggerB.GetComponent<BoxCollider2D>().enabled = false;
    }
}
