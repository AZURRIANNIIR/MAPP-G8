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




    private void Start()
    {
        musicAudioSource = musicAudioGameObject.GetComponent<AudioSource>();
        audioSourcesInScene = new List<AudioSource>();
        audioSourcesInScene.AddRange(FindObjectsOfType<AudioSource>());
        muteSoundEffectsToggle(PlayerPrefs.GetInt("SOUNDFX_MUTED") == 1);
        muteMusicToggle(PlayerPrefs.GetInt("MUSIC_MUTED") == 1);
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
            PlayerPrefs.SetInt("SOUNDFX_MUTED", 1);
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
            PlayerPrefs.SetInt("SOUNDFX_MUTED", 0);
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
            PlayerPrefs.SetInt("MUSIC_MUTED", 1);
        }
        else
        {
            musicAudioSource.Play();
            if (soundEffectsToggleGameObject.GetComponent<Toggle>().isOn == false)
            {
                audioToggleGameObject.GetComponent<Toggle>().isOn = false;
            }
            PlayerPrefs.SetInt("MUSIC_MUTED", 0);

        }
    }
}
