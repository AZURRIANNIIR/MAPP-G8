using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class AudioControllerScript : MonoBehaviour
{

    private const float PITCH_CHANGE_VALUE = 0.02f;
    private const float PITCH_MIN_VALUE = 1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        //Subscriba till Undo-knappens event automatiskt, så slipper vi göra det manuellt för varje scen.
        FindObjectOfType<UndoButton>().GetComponent<Button>().onClick.AddListener(() => DecreasePitch(PITCH_CHANGE_VALUE));
    }

    private void Update()
    {
        //Pitchen för vvåran AudioSource ska inte bli mindre än standardvärdet (Som bestäms av våran konstant)
        audioSource.pitch = Mathf.Clamp(audioSource.pitch,PITCH_MIN_VALUE, Mathf.Infinity);
    }

    #region Funktioner för pitchen
    public void IncreasePitch(float pitchValue)
    {
        audioSource.pitch += pitchValue;
    }

    public void DecreasePitch(float pitchValue)
    {
        audioSource.pitch -= pitchValue;    
    }
    #endregion

}
