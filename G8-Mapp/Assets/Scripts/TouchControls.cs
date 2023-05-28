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

    [Header("Attributes")]
    [SerializeField] private float movementLength;
    [Header("Components")]
    [SerializeField] private GameObject snake;
    [SerializeField] private TrailRenderer snakeTrailRenderer;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GridList gridListScript;
    private bool snakeCaught = false;
    [Header("Technical attributes")]
    [SerializeField] private float lerpFactor = 1000f;

    private Vector2 startPosition;

    private void Awake()
    {
        snakeTrailRenderer = GetComponent<TrailRenderer>();
        gridListScript = GetComponent<GridList>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = gameObject.transform.position;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(LMB_NUMBER) && RaycastIsHitting(mousePos))
        {
            snakeCaught = true;
            
        }

        if (Input.GetMouseButtonUp(LMB_NUMBER) && snakeCaught)
        {
            snakeCaught = false;
            ResetSnakePosition();
            snakeTrailRenderer.Clear();
        }

        if (snakeCaught)
        {
            //transform.position = Vector2.Lerp(transform.position, mousePos, lerpFactor);
            transform.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), lerpFactor);
        }
    }

    private bool RaycastIsHitting(Vector3 mousePos)
    {
        RaycastHit2D orm = Physics2D.Raycast(mousePos, Vector2.left, 0.05f, mask);
        if (orm.collider != null)
        {
            
            return true;
        }
        return false;
    }

    private void ResetSnakePosition()
    {
        GameObject mostRecentTile = gridListScript.GetMostRecentTile();
        transform.position = mostRecentTile != null ? mostRecentTile.transform.position : startPosition;
    }

}
