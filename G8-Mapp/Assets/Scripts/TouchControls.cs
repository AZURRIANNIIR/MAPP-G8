using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.Touch;
//using UnityEngine.InputSystem;

public class TouchControls : MonoBehaviour
{

    [SerializeField] private float movementLength;
    [SerializeField] private GameObject snake;
    [SerializeField] private LayerMask mask;
    private bool snakeCaught = false;

    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        width = (float)Screen.width;
        height = (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(0) && RaycastIsHitting(mousePos))
        {
            snakeCaught = true;
            print("mus ner");
        }

        if (Input.GetMouseButtonUp(0))
        {
            snakeCaught = false;
        }

        if (snakeCaught)
        {
            transform.position = Vector2.Lerp(transform.position, mousePos, 1000f);

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

}
