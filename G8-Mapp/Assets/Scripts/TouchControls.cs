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
    [SerializeField] private float margin = 5f;

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
        //TouchSimulation.Enable();
        //PlayerInput.SwitchCurrentControlScheme(InputSystem.devices.First(d => d == Touchscreen.current));


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = Vector2.Lerp(transform.position, mousePos, 0.1f);
            print("mus ner");

            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 100))
            //{
            //    if (hit.collider.tag == "snake")
            //    {
            //        snake.transform.position = mousePos;
            //        print("Orm träffad");
            //    }

            //}


            //if (((snake.transform.position.x + margin) >= mousePos.x ) && ()>= (snake.transform.position.x - margin)) && (mousePos.x <= () && (mousePos.y >= snake.transform.position.y - margin) && (mousePos.y <= snake.transform.position.y + margin)))
            //{
            //    print("rör ormen");
            //}

        }

        //if (Input.touchCount > 0)
        //{

        //    print("touch");

        //    Touch touch = Input.GetTouch(0);

        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPosition.z = 0f;
        //    transform.position = touchPosition;

        //    Vector2 startPosition = touch.position;

        //    //if ((startPosition.x >= (snake.transform.position.x - margin)) && (startPosition.x <= (snake.transform.position.x + margin) && (startPosition.y >= snake.transform.position.y - margin) && (startPosition.y <= snake.transform.position.y + margin)))
        //    //{
        //    //    print("rörd");
        //    //}
        //}

        //            if ((mousePos.x >= (snake.transform.position.x - margin)) && (mousePos.x <= (snake.transform.position.x + margin) && (mousePos.y >= snake.transform.position.y - margin) && (mousePos.y <= snake.transform.position.y + margin)))

    }

}
