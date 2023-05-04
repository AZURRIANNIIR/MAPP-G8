using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioControllerScript : MonoBehaviour
{

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region Funktioner f�r pitchen
    public void IncreasePitch(float pitch)
    {
        audioSource.pitch += pitch;
    }

    public void DecreasePitch(float pitch)
    {
        audioSource.pitch -= pitch;    
    }
    #endregion

}
