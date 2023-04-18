using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.Touch;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(TrailRenderer))]

public class TouchControls : MonoBehaviour
{
    private const int LMB_NUMBER = 0;

    [SerializeField] private float movementLength;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    private bool snakeCaught = false;
    [SerializeField] private float lerpFactor = 1000f;

    private float width;
    private float height;

    private Vector2 startPosition;

    private void Awake()
    {
        snakeTrailRenderer = GetComponent<TrailRenderer>();
        width = Screen.width;
        height = Screen.height;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(LMB_NUMBER) && RaycastIsHitting(mousePos))
        {
            snakeCaught = true;
            print("mus ner");
        }

        if (Input.GetMouseButtonUp(LMB_NUMBER) && snakeCaught)
        {
            snakeCaught = false;
            ResetSnakeToStart();
            snakeTrailRenderer.Clear();
        }

        if (snakeCaught)
        {
            transform.position = Vector2.Lerp(transform.position, mousePos, lerpFactor);
        }
    }

    private bool RaycastIsHitting(Vector3 mousePos)
    {
        RaycastHit2D orm = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, mask);
        if (orm.collider != null)
        {
            print("orm träffad");
            return true;
        }
        return false;
    }

    private void ResetSnakeToStart()
    {
        transform.position = startPosition;
    }

}
