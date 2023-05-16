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
        if (rotationList.Count > 0 ) 
        {
            Debug.Log("Dictionaryn kan användas");
        }
        Debug.Log(transform.parent.gameObject.name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStartRotation();
    }

    private void LateUpdate()
    {
        currentPos = transform.position;
    }

    private void SetStartRotation()
    {
        transform.localRotation = Quaternion.Euler(rotationList[startRotation]);
    }

    private void SetRotation(Vector3 vectorToBaseRotationOn)
    {
        Debug.Log("Huvudet ska sätta sin rotation nu");
        Debug.Log("Current position:" + currentPos);
        Debug.Log("Vector argument:" + vectorToBaseRotationOn);
        //Till höger
        if (Mathf.RoundToInt(currentPos.x) < Mathf.RoundToInt(vectorToBaseRotationOn.x))
        {
            transform.localRotation = Quaternion.Euler(rotationList[rotations.right]);
        }
        //Till vänster
        if (Mathf.RoundToInt(currentPos.x) > Mathf.RoundToInt(vectorToBaseRotationOn.x))
        {
            transform.localRotation = Quaternion.Euler(rotationList[rotations.left]);
        }
        //Nedåt
        if (Mathf.RoundToInt(currentPos.y) > Mathf.RoundToInt(vectorToBaseRotationOn.y))
        {
            transform.localRotation = Quaternion.Euler(rotationList[rotations.down]);
        }
        //Uppåt
        if (Mathf.RoundToInt(currentPos.y) < Mathf.RoundToInt(vectorToBaseRotationOn.y))
        {
            transform.localRotation = Quaternion.Euler(rotationList[rotations.up]);
        }
    }

    #region Enable/Disable-funktioner
    private void OnEnable()
    {
        SnakeMovement.OnReturnToStart += SetStartRotation;
        SnakeMovement.OnMovement += SetRotation;
    }

    private void OnDisable()
    {
        SnakeMovement.OnReturnToStart -= SetStartRotation;
        SnakeMovement.OnMovement -= SetRotation;
    }
    #endregion
}
