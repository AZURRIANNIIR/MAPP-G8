using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SnakeHead : MonoBehaviour
{
    private enum startRotations { left, right, up, down }
    [SerializeField] private startRotations startRotation; 

    private float ROTATION_VALUE = 90f;

    private SnakeMovement movementScript;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        movementScript = GetComponentInParent<SnakeMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        switch (startRotation)
        {
            case startRotations.left:
                transform.localRotation = Quaternion.Euler(0f,0f,-ROTATION_VALUE); 
                break;
            case startRotations.right:
                transform.localRotation = Quaternion.Euler(0f, 0f, ROTATION_VALUE); 
                break;
            case startRotations.up:
                transform.localRotation = Quaternion.Euler(0, 0, ROTATION_VALUE * 2f);
                break;
            case startRotations.down:
                transform.localRotation = Quaternion.Euler(0, 0, 0f);
                break;
        }
    }
}
