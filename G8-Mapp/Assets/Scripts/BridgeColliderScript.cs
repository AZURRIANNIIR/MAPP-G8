using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeColliderScript : ColliderScript
{
    [SerializeField] private Sprite bridgeTakenOnceSprite;

    public void ChangeBridgeSpriteToTaken()
    {
        ChangeGridSprite(bridgeTakenOnceSprite);
    }

    public override void DisableTile()
    {
        base.DisableTile();
        //Typkonvertering anv�nds h�r f�r att f�rs�kra oss om att vi anv�nder korsningsfunktionaliteten
        BridgeTile bridgeTile = (BridgeTile)gridTile;
        bridgeTile.SetCrossedOnceStatus(false);
    }

    public override void ResetTile()
    {
        ResetBridgeTile();
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
            ChangeGridSprite(TileStartSprite);
            return;
        }
    }
    public void SetBridgeSpriteToStartSprite()
    {
        ChangeGridSprite(TileStartSprite);
    }
}
