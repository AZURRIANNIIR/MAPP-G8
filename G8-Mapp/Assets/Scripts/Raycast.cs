using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).

    [SerializeField] private LayerMask mask;

    Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Cast a ray straight down.
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 1f, mask);

        // If it hits something...
        if (right.collider != null)
        {
            print("Right");
            Debug.DrawRay(transform.position, Vector2.right, Color.green);
        }

        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 1f, mask);

        // If it hits something...
        if (left.collider != null)
        {
            print("Left");
        }

        RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, 1f, mask);

        // If it hits something...
        if (up.collider != null)
        {
            print("Up");
        }

        RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, 1f, mask);

        // If it hits something...
        if (down.collider != null)
        {
            print("Down");
        }

        //rayca in = Physics2D.Raycast(transform.position, new Vector3(0,0,-1));

        //// If it hits something...
        //if (down.collider != null)
        //{
        //    print("Down");
        //}

    }
}
