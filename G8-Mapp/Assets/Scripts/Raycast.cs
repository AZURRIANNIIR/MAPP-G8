using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    [SerializeField] private LayerMask mask;

    void Start()
    {
    }

    //void FixedUpdate()
    //{

    //    RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 1f, mask);
    //    if (right.collider != null)
    //    {
    //        print("Right");
    //        Debug.DrawRay(transform.position, Vector2.right, Color.green);
    //    }

    //    RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 1f, mask);
    //    if (left.collider != null)
    //    {
    //        print("Left");
    //    }

    //    RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, 1f, mask);
    //    if (up.collider != null)
    //    {
    //        print("Up");
    //    }

    //    RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, 1f, mask);
    //    if (down.collider != null)
    //    {
    //        print("Down");
    //    }
    //}
}
