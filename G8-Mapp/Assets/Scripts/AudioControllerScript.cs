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

        //Subscriba till Undo-knappens event automatiskt, s� slipper vi g�ra det manuellt f�r varje scen.
        FindObjectOfType<UndoButton>().GetComponent<Button>().onClick.AddListener(() => DecreasePitch(PITCH_CHANGE_VALUE));
    }

    private void Update()
    {
        //Pitchen f�r vv�ran AudioSource ska inte bli mindre �n standardv�rdet (Som best�ms av v�ran konstant)
        audioSource.pitch = Mathf.Clamp(audioSource.pitch,PITCH_MIN_VALUE, Mathf.Infinity);
    }

    #region Funktioner f�r pitchen
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
