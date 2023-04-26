using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vi ärver här från GridTile-skriptet
public class BridgeTile : GridTile
{
    [SerializeField] private bool crossedOnce;

    new private void Start()
    {
        base.Start();
        crossedOnce = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Om vi redan har korsat bron en gång
        if (collision.gameObject.CompareTag("Snake") && crossedOnce && !taken)
        {
            taken = true;
            print("ny plats");
            tileCollider.TakeTile();
            gameController.tileTaken();
            return;
        }

        //Om bron korsas för första gången
        else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !taken)
        {
            tileCollider.ChangeBridgeColor();
            crossedOnce = true;
            print("bridge taken once");
        }
    }

    public bool GetCrossedOnceStatus()
    {
        return crossedOnce;
    }

    public void SetCrossedOnceStatus(bool state)
    {
        crossedOnce = state;
    }

    //Samma problem som för SetTakenStatus i parent-skriptet, grundimplementationen fungerar inte med våran Event från Clear-Button
    private void SetCrossedOnceStatusToFalse() => SetCrossedOnceStatus(false);

    #region Enable/Disable-funktioner
    new private void OnEnable()
    {
        base.OnEnable();
        ClearButton.OnClick += SetCrossedOnceStatusToFalse;
    }

    new private void OnDisable()
    {
        base.OnDisable();
        ClearButton.OnClick -= SetCrossedOnceStatusToFalse;
    }
    #endregion
}

