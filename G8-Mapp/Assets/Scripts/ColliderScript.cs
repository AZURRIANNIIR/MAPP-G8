using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    [SerializeField] private GridTile gridTile;
    [SerializeField] private Sprite tileTakenSprite;
    [SerializeField] private Sprite tileStartSprite;
    [SerializeField] private Sprite tileDisabledSprite;
    [SerializeField] private Sprite bridgeTakenOnceSprite;
    [SerializeField] private BridgeTile bridgeTile;
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
        spriteRenderer.sprite = tileStartSprite;
        if (childObject.CompareTag("BridgeTile"))
        {
            bridgeTile = GetComponentInChildren<BridgeTile>();
        }
    }

    public void TakeTile()
    {

        print("ruta tagen");
        spriteRenderer.sprite = tileTakenSprite;
    }

    public void ChangeBridgeColor()
    {
        spriteRenderer.sprite = bridgeTakenOnceSprite;
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
            spriteRenderer.sprite = tileStartSprite;
        }

        if (childObject.CompareTag("BridgeTile"))
        {
            if (bridgeTile.GetTakenStatus())
            {
                bridgeTile.SetTakenStatus(false);
                spriteRenderer.sprite = bridgeTakenOnceSprite;
                return;
            }
            else
            {
                bridgeTile.SetCrossedOnceStatus(false);
                spriteRenderer.sprite = tileStartSprite;
            }
        }
    }

    public void disableTile()
    {
        boxCollider.enabled = true;
        spriteRenderer.sprite = tileDisabledSprite;

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

    public void SetBridgeColorToStartColor()
    {
        spriteRenderer.sprite= tileStartSprite;
    }
}



