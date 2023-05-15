using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SnakeHead : MonoBehaviour
{
    private enum startRotations { left, right, up, down }
    [SerializeField] private startRotations startRotation;

    private const float ROTATION_VALUE = 90f;

    private SnakeMovement movementScript;

    [SerializeField] private Vector3 currentPos;

    private void Awake()
    {
        movementScript = GetComponentInParent<SnakeMovement>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        switch (startRotation)
        {
            case startRotations.left:
                transform.localRotation = Quaternion.Euler(0f, 0f, -ROTATION_VALUE);
                break;
            case startRotations.right:
                transform.localRotation = Quaternion.Euler(0f, 0f, ROTATION_VALUE);
                break;
            case startRotations.up:
                transform.localRotation = Quaternion.Euler(0f, 0f, ROTATION_VALUE * 2f);
                break;
            case startRotations.down:
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
        }
    }

    private void Update()
    {
        //Till öst
        if (currentPos.x < movementScript.CurrentPosition.x)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, ROTATION_VALUE);
        }
        //Till väst
        if (currentPos.x > movementScript.CurrentPosition.x)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, -ROTATION_VALUE);
        }
        //Till söder
        if (currentPos.y > movementScript.CurrentPosition.y)
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        //Till norr
        if (currentPos.y < movementScript.CurrentPosition.y)
        {
            transform.localRotation = Quaternion.Euler(0, 0, ROTATION_VALUE * 2f);
        }
    }

    private void LateUpdate()
    {
        currentPos = transform.position;
    }
}
