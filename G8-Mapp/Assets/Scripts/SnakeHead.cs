using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SnakeHead : MonoBehaviour
{
    private const float ROTATION_VALUE = 90f;

    private enum Rotations { left, right, up, down }
    [SerializeField] private Rotations startRotation;

    [SerializeField] private Vector3 currentPos;

    [SerializeField] private List<Quaternion> levelRotations;

    //Readonly f�r att f�rhindra alla former av modifiering. Det skulle annars m�jligg�ra att spelet slutar fungera som det �r t�nkt.
    private readonly Dictionary<Rotations, Vector3> rotationList = new Dictionary<Rotations, Vector3>
        {
            { Rotations.left, new Vector3(0f,0f, ROTATION_VALUE * 3f)},
            { Rotations.right, new Vector3(0f, 0f, ROTATION_VALUE)},
            { Rotations.up, new Vector3(0f, 0f, ROTATION_VALUE * 2f)},
            { Rotations.down, Vector3.zero }
        };

    private void Awake()
    {
        if (rotationList.Count > 0 ) 
        {
            Debug.Log(name + ": Dictionaryn kan anv�ndas");
        }
        Debug.Log(transform.parent.gameObject.name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStartRotation();
    }

    private void LateUpdate()
    {
        currentPos = transform.position;
    }

    private void SetStartRotation()
    {
        transform.localRotation = Quaternion.Euler(rotationList[startRotation]);
    }

    private void SetRotation(Vector3 vectorToBaseRotationOn)
    {
        Debug.Log("Huvudet ska s�tta sin rotation nu");
        Debug.Log("Current position:" + currentPos);
        Debug.Log("Vector argument:" + vectorToBaseRotationOn);
        //Till h�ger
        if (Mathf.RoundToInt(currentPos.x) < Mathf.RoundToInt(vectorToBaseRotationOn.x))
        {
            transform.localRotation = Quaternion.Euler(rotationList[Rotations.right]);
        }
        //Till v�nster
        if (Mathf.RoundToInt(currentPos.x) > Mathf.RoundToInt(vectorToBaseRotationOn.x))
        {
            transform.localRotation = Quaternion.Euler(rotationList[Rotations.left]);
        }
        //Ned�t
        if (Mathf.RoundToInt(currentPos.y) > Mathf.RoundToInt(vectorToBaseRotationOn.y))
        {
            transform.localRotation = Quaternion.Euler(rotationList[Rotations.down]);
        }
        //Upp�t
        if (Mathf.RoundToInt(currentPos.y) < Mathf.RoundToInt(vectorToBaseRotationOn.y))
        {
            transform.localRotation = Quaternion.Euler(rotationList[Rotations.up]);
        }
        if (!UndoButton.EventFired)
        {
            levelRotations.Add(transform.localRotation);
        }  
    }

    private void UndoRotationAction()
    {
        //Steg 1
        levelRotations.RemoveAt(levelRotations.Count -1);

        //Steg 2; titta om listan är tom innan vi försöker att rotera ormen;
        if (levelRotations.Count < 1)
        {
            Debug.Log(this.GetType().Name + ": Det finns inga sparade rotationer");
            return;
        }

        //Steg 3: Applicera i så fall rotationen.
        transform.localRotation = levelRotations[levelRotations.Count - 1];
    }

    public float GetZRotation()
    {
        return transform.localRotation.eulerAngles.z;
    }

    #region Enable/Disable-funktioner
    private void OnEnable()
    {
        SnakeMovement.OnReturnToStart += SetStartRotation;
        SnakeMovement.OnMovement += SetRotation;
        UndoButton.OnClick += UndoRotationAction;
    }

    private void OnDisable()
    {
        SnakeMovement.OnReturnToStart -= SetStartRotation;
        SnakeMovement.OnMovement -= SetRotation;
        UndoButton.OnClick -= UndoRotationAction;
    }
    #endregion
}
