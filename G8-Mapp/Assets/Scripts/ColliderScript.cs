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

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (gameObject.tag == "GridTile")
        {
            gridTile = GetComponentInChildren<GridTile>();
        }
        if (gameObject.tag == "BridgeTile")
        {
            bridgeTile = GetComponentInChildren<BridgeTile>();
        }

        spriteRenderer.color = tileStartColor;
        boxCollider.enabled = false;

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
        spriteRenderer.color = tileStartColor;

        if (gameObject.tag == "GridTile")
        {
            gridTile.SetTakenStatus(false);
        }

        if (gameObject.tag == "BridgeTile")
        {
            bridgeTile.SetCrossedOnceStatus(false);
            bridgeTile.SetTakenStatus(false);
        }
    }

    public void disableTile()
    {
        boxCollider.enabled = true;
        spriteRenderer.color = tileDisabledColor;

        if (gameObject.tag == "GridTile")
        {
            gridTile.SetTakenStatus(false);
        }

        if (gameObject.tag == "BridgeTile")
        {
            bridgeTile.SetCrossedOnceStatus(false);
            bridgeTile.SetTakenStatus(false);
        }
    }
}



