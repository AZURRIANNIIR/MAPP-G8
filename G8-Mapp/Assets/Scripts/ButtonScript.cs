using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
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
            print("knapp tagen");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            for (int i = 0; i < tileList.Count; i++)
            {
                tileList[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
