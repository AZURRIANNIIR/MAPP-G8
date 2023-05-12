using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeColliderScript : ColliderScript
{
    [SerializeField] private Sprite bridgeTakenOnceSprite;

    new private void Start()
    {
        base.Start();
        if (tileScript.GetType() != typeof(BridgeTile))
        {
            Debug.LogWarning("Varning, " + gameObject.name + "'s gridTile �r inte av typen BridgeTile. D�rmed kommer inte specifik BridgeTile-funktionalitet att fungera.");
        }
    }

    public void SetBridgeSpriteToStartSprite()
    {
        ChangeGridSprite(TileStartSprite);
    }

    public void ChangeBridgeSpriteToTaken()
    {
        ChangeGridSprite(bridgeTakenOnceSprite);
    }

    public override void DisableTile()
    {
        base.DisableTile();
        //Typkonvertering anv�nds h�r f�r att f�rs�kra oss om att vi anv�nder korsningsfunktionaliteten
        BridgeTile bridgeTile = (BridgeTile)tileScript;
        bridgeTile.SetCrossedOnceStatus(false);
        bridgeTile.SetTakenStatus(false);
    }

    public override void ResetTile()
    {
        ResetBridgeTile();
    }

    private void ResetBridgeTile()
    {
        //Typkonvertering anv�nds h�r f�r att f�rs�kra oss om att vi anv�nder korsningsfunktionaliteten
        BridgeTile bridgeTile = (BridgeTile)tileScript;
        if (bridgeTile.GetTakenStatus())
        {
            bridgeTile.SetTakenStatus(false);
            ChangeGridSprite(bridgeTakenOnceSprite);
            return;
        }
        else
        {
            bridgeTile.SetCrossedOnceStatus(false);
            ChangeGridSprite(TileStartSprite);
            return;
        }
    }
}
