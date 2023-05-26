using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{

    private AudioSource musicAudioSource;
    private List<AudioSource> audioSourcesInScene;

    [SerializeField] GameObject musicAudioGameObject;
    [SerializeField] GameObject musicToggleGameObject;
    [SerializeField] GameObject soundEffectsToggleGameObject;
    [SerializeField] GameObject audioToggleGameObject;




    private void Awake()
    {
        musicAudioSource = musicAudioGameObject.GetComponent<AudioSource>();
        audioSourcesInScene = new List<AudioSource>();
        audioSourcesInScene.AddRange(FindObjectsOfType<AudioSource>());
    }

    public void muteSoundEffectsToggle(bool muted)
    {
        if (muted == true)
        {
            foreach (AudioSource audioSource in audioSourcesInScene)
            {
                if (audioSource.gameObject.tag != "MusicSource")
                {
                    audioSource.mute = true;
                }
            }

            if (musicToggleGameObject.GetComponent<Toggle>().isOn == true)
            {
                audioToggleGameObject.GetComponent<Toggle>().isOn = true;
            }


        }
        else
        {
            foreach (AudioSource audioSource in audioSourcesInScene)
            {
                if (audioSource.gameObject.tag != "MusicSource")
                {
                    audioSource.mute = false;
                }
            }
            if (musicToggleGameObject.GetComponent<Toggle>().isOn == false)
            {
                audioToggleGameObject.GetComponent<Toggle>().isOn = false;
            }
        }

    }


    public void muteAudioToggle(bool muted)
    {
        if (muted)
        {
            //muteMusicToggle(true);
            //muteSoundEffectsToggle(true);
            musicToggleGameObject.GetComponent<Toggle>().isOn = true;
            soundEffectsToggleGameObject.GetComponent<Toggle>().isOn = true;


        }
        else
        {
            //muteMusicToggle(false);
            //muteSoundEffectsToggle(false);
            musicToggleGameObject.GetComponent<Toggle>().isOn = false;
            soundEffectsToggleGameObject.GetComponent<Toggle>().isOn = false;
        }

    }

    public void muteMusicToggle(bool muted)
    {
        if (muted == true)
        {
            musicAudioSource.Stop();
            if (soundEffectsToggleGameObject.GetComponent<Toggle>().isOn == true)
            {
                audioToggleGameObject.GetComponent<Toggle>().isOn = true;
            }
        }
        else
        {
            musicAudioSource.Play();
            if (soundEffectsToggleGameObject.GetComponent<Toggle>().isOn == false)
            {
                audioToggleGameObject.GetComponent<Toggle>().isOn = false;
            }
        }
    }
}
