using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject Snake;
    private bool OnBridge = false;

    


    void SetSortingLayer(string layerName)
    {
        Snake.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
    }

    void UpdateSortingOrder()
    {
        if (OnBridge)
        {
            SetSortingLayer("Bridge");
        }
        else
        {
            SetSortingLayer("Default");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TileTrigger"))
        {
            OnBridge = false;
            UpdateSortingOrder();
        }
        else if (collision.CompareTag("BridgeTrigger"))
        {
            OnBridge = true;
            UpdateSortingOrder();
        }
        
        
    }
}
