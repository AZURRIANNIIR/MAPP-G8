using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
