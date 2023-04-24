using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    private BoxCollider2D _bc;

    // Start is called before the first frame update
    void Start()
    {
        _bc = gameObject.GetComponent<BoxCollider2D>();
    }

    public void TakeTile()
    {
        _bc.enabled = true;
        print("ruta tagen");
    }

}
