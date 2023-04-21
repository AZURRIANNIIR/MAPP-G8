using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    GridTile gridTile;
    [SerializeField] private Color tileTakenColor;
    [SerializeField] private Color tileStartColor;
    [SerializeField] private Color tileDisabledColor;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gridTile = GetComponentInChildren<GridTile>();
    }

    public void TakeTile()
    {
        
        print("ruta tagen");
        spriteRenderer.color = tileTakenColor;
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

    public void disableTile()
    {
        boxCollider.enabled = true;
        spriteRenderer.color = tileDisabledColor;
        gridTile.SetTakenStatus(false);
    }
}
