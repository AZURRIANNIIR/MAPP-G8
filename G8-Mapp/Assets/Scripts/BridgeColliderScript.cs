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
            Debug.LogWarning("Varning, " + gameObject.name + "'s gridTile är inte av typen BridgeTile. Därmed kommer inte specifik BridgeTile-funktionalitet att fungera.");
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
        //Typkonvertering används här för att försäkra oss om att vi använder korsningsfunktionaliteten
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
        //Typkonvertering används här för att försäkra oss om att vi använder korsningsfunktionaliteten
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
