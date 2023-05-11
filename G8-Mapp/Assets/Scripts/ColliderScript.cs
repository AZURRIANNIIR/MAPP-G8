using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]

public abstract class ColliderScript : MonoBehaviour
{
    private const string CHILDOBJECT_NAME = "TriggerBox";
    protected BoxCollider2D boxCollider;

    [Header("Scripts")]
    [SerializeField] protected GridTile gridTile;

    [Header("Gameobjects")]
    [SerializeField] protected GameObject childObject;

    [Header("Sprites")]
    [SerializeField] private Sprite tileTakenSprite;
    [SerializeField] private Sprite tileStartSprite;
    [SerializeField] private Sprite tileDisabledSprite;

    #region Properties för Sprites
    protected Sprite TileTakenSprite
    {
        get { return tileTakenSprite; }
        private set
        {
            tileTakenSprite = value;
        }
    }
    protected Sprite TileStartSprite
    {
        get { return tileStartSprite; }
        private set
        {
            tileStartSprite = value;
        }
    }
    protected Sprite TileDisabledSprite
    {
        get { return tileDisabledSprite; }
        private set
        {
            tileDisabledSprite = value;
        }
    }
    #endregion

    protected SpriteRenderer spriteRenderer;

    //Awake is called before the first frame update, similiar to Start
    //Unlike Start, it's called even if the object is disabled
    protected void Awake()
    {
        if (!childObject)
        {
            childObject = gameObject.transform.Find(CHILDOBJECT_NAME).gameObject;
        }
    }

    protected void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = TileStartSprite;
        boxCollider = GetComponent<BoxCollider2D>();
        gridTile = gameObject.GetComponentInChildren<GridTile>();
    }


    protected void ChangeGridSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void TakeTile()
    {
        print("ruta tagen");
        spriteRenderer.sprite = TileTakenSprite;
    }

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }

    public abstract void ResetTile();

    public virtual void DisableTile()
    {
        EnableCollider();
    }
}
