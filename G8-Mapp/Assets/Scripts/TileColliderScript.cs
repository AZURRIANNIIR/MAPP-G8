using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TileColliderScript : ColliderScript
{
    public override void ResetTile()
    {
        EnableCollider();
        ResetGridTile();
    }

    #region "Reset tile" funktioner
    private void ResetGridTile()
    {
        gridTile.SetTakenStatus(false);
        ChangeGridSprite(TileStartSprite);
    }
    #endregion

    public override void DisableTile()
    {
        base.DisableTile();
        gridTile.SetTakenStatus(false);
    }
}



