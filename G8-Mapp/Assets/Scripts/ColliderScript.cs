using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    [SerializeField] private GridTile gridTile;
    [SerializeField] private Color tileTakenColor;
    [SerializeField] private Color tileStartColor;
    [SerializeField] private Color bridgeTakenOnceColor;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gridTile = GetComponentInChildren<GridTile>();
        spriteRenderer.color = tileStartColor;
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

    public void enableCollider()
    {
        boxCollider.enabled = true;
    }

    public void resetTile()
    {
        boxCollider.enabled = false;
        spriteRenderer.color = tileStartColor;
        gridTile.SetTakenStatus(false);
    }
}
