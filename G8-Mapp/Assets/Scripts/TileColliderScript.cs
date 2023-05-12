using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TileColliderScript : ColliderScript
{
    public override void ResetTile()
    {
        DisableCollider();
        ResetGridTile();
    }

    private void ResetGridTile()
    {
        tileScript.SetTakenStatus(false);
        ChangeGridSprite(TileStartSprite);
    }

    public override void DisableTile()
    {
        base.DisableTile();
        tileScript.SetTakenStatus(false);
    }
}



