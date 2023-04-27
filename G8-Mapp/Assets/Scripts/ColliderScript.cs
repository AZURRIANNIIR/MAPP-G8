using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    [SerializeField] private GridTile gridTile;
    [SerializeField] private Color tileTakenColor;
    [SerializeField] private Color tileStartColor;
    [SerializeField] private Color tileDisabledColor;
    [SerializeField] private Color bridgeTakenOnceColor;
    private BridgeTile bridgeTile;
    [SerializeField] GameObject childObject;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (!childObject)
        {
            childObject = gameObject.transform.Find("TriggerBox").gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gridTile = GetComponentInChildren<GridTile>();
        spriteRenderer.color = tileStartColor;
        if (childObject.CompareTag("BridgeTile"))
        {
            bridgeTile = GetComponentInChildren<BridgeTile>();
        }
    }

    public void TakeTile()
    {

        print("ruta tagen");
        spriteRenderer.color = tileTakenColor;
    }

    public void ChangeBridgeColor()
    {
        spriteRenderer.color = bridgeTakenOnceColor;
    }

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    public void ResetTile()
    {
        boxCollider.enabled = false;

        if (childObject.CompareTag("GridTile"))
        {
            gridTile.SetTakenStatus(false);
            spriteRenderer.color = tileStartColor;
        }

        if (childObject.CompareTag("BridgeTile"))
        {
            if (bridgeTile.GetTakenStatus())
            {
                bridgeTile.SetTakenStatus(false);
                spriteRenderer.color = bridgeTakenOnceColor;
                return;
            }
            else
            {
                bridgeTile.SetCrossedOnceStatus(false);
                spriteRenderer.color = tileStartColor;
            }
        }
    }

    public void disableTile()
    {
        boxCollider.enabled = true;
        spriteRenderer.color = tileDisabledColor;

        if (childObject.tag == "GridTile")
        {
            gridTile.SetTakenStatus(false);
        }

        if (childObject.tag == "BridgeTile")
        {
            bridgeTile.SetCrossedOnceStatus(false);
            bridgeTile.SetTakenStatus(false);
        }
    }
}



