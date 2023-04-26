using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridgeTile : MonoBehaviour
{

    public bool crossedOnce;
    public bool taken = false;
    [SerializeField] private ColliderScript tileCollider;
    [SerializeField] private SnakeMovement snakeMovement;
    [SerializeField] private GameController gameController;
    [SerializeField] private LayerMask mask;

    [SerializeField] private GameObject upperTriggerCollider;
    [SerializeField] private GameObject lowerTriggerCollider;
    [SerializeField] private GameObject leftTriggerCollider;
    [SerializeField] private GameObject rightTriggerCollider;

    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    private bool collidersEnabled;



    private void Start()
    {
        crossedOnce = false;
    }

    private void Update()
    {
        if (!snakeMovement.enteredHorizontally)
        {
            turnOnPath(upperBoxCollider, lowerBoxCollider, upperTriggerCollider, lowerTriggerCollider);
        }

        if (!snakeMovement.enteredHorizontally)
        {
            turnOnPath(leftBoxCollider, rightBoxCollider, leftTriggerCollider, rightTriggerCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Om vi redan har korsat bron en gång
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !taken)
        {
            taken = true;
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
            return;
        }

        //Om bron korsas för första gången
        else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !taken)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && taken)
        {
            tileCollider.EnableCollider();
        }
    }

    public bool GetCrossedOnce()
    {
        return crossedOnce;
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }

    public void SetCrossedOnceStatus(bool state)
    {
        crossedOnce = state;
    }

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

