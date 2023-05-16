using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SnakeHead : MonoBehaviour
{
    private const float ROTATION_VALUE = 90f;

    private enum rotations { left, right, up, down }
    [SerializeField] private rotations startRotation;

    private SnakeMovement movementScript;

    [SerializeField] private Vector3 currentPos;

    //Readonly för att förhindra alla former av modifiering. Det skulle annars möjliggöra att spelet slutar fungera som det är tänkt.
    private readonly Dictionary<rotations, Vector3> rotationList = new Dictionary<rotations, Vector3>
        {
            { rotations.left, new Vector3(0f,0f, -ROTATION_VALUE)},
            { rotations.right, new Vector3(0f, 0f, ROTATION_VALUE)},
            { rotations.up, new Vector3(0f, 0f, ROTATION_VALUE * 2f)},
            { rotations.down, Vector3.zero }
        };

    private void Awake()
    {
        movementScript = GetComponentInParent<SnakeMovement>();
        if (rotationList.Count > 0 ) 
        {
            Debug.Log("Dictionaryn kan användas");
        }
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStartRotation();
    }

    private void Update()
    {
        if (!UndoButton.EventFired)
        {
            //Till höger
            if (currentPos.x < movementScript.CurrentPosition.x)
            {
                transform.localRotation = Quaternion.Euler(rotationList[rotations.right]);
            }
            //Till vänster
            if (currentPos.x > movementScript.CurrentPosition.x)
            {
                transform.localRotation = Quaternion.Euler(rotationList[rotations.left]);
            }
            //Nedåt
            if (currentPos.y > movementScript.CurrentPosition.y)
            {
                transform.localRotation = Quaternion.Euler(rotationList[rotations.down]);
            }
            //Uppåt
            if (currentPos.y < movementScript.CurrentPosition.y)
            {
                transform.localRotation = Quaternion.Euler(rotationList[rotations.up]);
            }
        }

    }

    private void LateUpdate()
    {
        currentPos = transform.position;
    }

    private void SetStartRotation()
    {
        Debug.Log("Sätter start-rotation för huvudet");
        transform.localRotation = Quaternion.Euler(rotationList[startRotation]);
    }

    #region Enable/Disable-funktioner
    private void OnEnable()
    {
        SnakeMovement.OnReturnToStart += SetStartRotation;
    }

    private void OnDisable()
    {
        SnakeMovement.OnReturnToStart -= SetStartRotation;
    }
    #endregion
}
