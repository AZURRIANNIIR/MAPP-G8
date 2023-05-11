using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private const string CHILDOBJECT_NAME = "TriggerBox";

    private BoxCollider2D boxCollider;
    [Header("Scripts")]
    [SerializeField] private GridTile gridTile;
    [Header("Sprites")]
    [SerializeField] private Sprite tileTakenSprite;
    [SerializeField] private Sprite tileStartSprite;
    [SerializeField] private Sprite tileDisabledSprite;
    [SerializeField] private Sprite bridgeTakenOnceSprite;
    [Header("Gameobjects")]
    [SerializeField] private GameObject childObject;

    private SpriteRenderer spriteRenderer;

    //Awake is called before the first frame update, similiar to Start
    //Unlike Start, it's called even if the object is disabled
    void Awake()
    {
        if (!childObject)
        {
            childObject = gameObject.transform.Find(CHILDOBJECT_NAME).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gridTile = GetComponentInChildren<GridTile>();
        spriteRenderer.sprite = tileStartSprite;
    }

    public void TakeTile()
    {
        print("ruta tagen");
        spriteRenderer.sprite = tileTakenSprite;
    }

    private void ChangeGridSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ChangeBridgeSpriteToTaken()
    {
        ChangeGridSprite(bridgeTakenOnceSprite);
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
            ResetGridTile();
        }

        if (childObject.CompareTag("BridgeTile"))
        {
            ResetBridgeTile();
        }
    }

    #region "Reset tile" funktioner
    private void ResetGridTile()
    {
        gridTile.SetTakenStatus(false);
        ChangeGridSprite(tileStartSprite);
    }

    private void ResetBridgeTile()
    {
        BridgeTile bridgeTile = (BridgeTile)gridTile;
        if (bridgeTile.GetTakenStatus())
        {
            bridgeTile.SetTakenStatus(false);
            ChangeGridSprite(bridgeTakenOnceSprite);
            return;
        }
        else
        {
            bridgeTile.SetCrossedOnceStatus(false);
            ChangeGridSprite(tileStartSprite);
            return;
        }
    }
    #endregion

    public void disableTile()
    {
        boxCollider.enabled = true;
        SetBridgeSpriteToStartSprite();
        
        gridTile.SetTakenStatus(false);

        //Är det en bro som är colliderns GridTile?
        if (gridTile.GetType() == typeof(BridgeTile))
        {
            BridgeTile bridgeTile = (BridgeTile)gridTile;
            bridgeTile.SetCrossedOnceStatus(false);
        }
    }

    public void SetBridgeSpriteToStartSprite()
    {
        ChangeGridSprite(tileStartSprite);
    }
}



