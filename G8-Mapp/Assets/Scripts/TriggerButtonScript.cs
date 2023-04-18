using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake"))
        {
            for (int i = 0; i< tileList.Count; i++)
            {
                tileList[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }
}
