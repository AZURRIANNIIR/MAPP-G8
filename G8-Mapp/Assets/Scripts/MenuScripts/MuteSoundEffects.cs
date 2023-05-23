using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSoundEffects : MonoBehaviour
{
    public List<AudioSource> audioSourcesInScene;

    private void Start()
    {
        audioSourcesInScene = new List<AudioSource>();
        audioSourcesInScene.AddRange(FindObjectsOfType<AudioSource>());
    }

    public void MuteToggleOn(bool muted)
    {
        if(muted == true)
        {
            foreach (AudioSource audioSource in audioSourcesInScene)
            {
                if(audioSource.gameObject.tag != "AudioController")
                {
                    audioSource.mute = true;
                }
            }

        } else
        {
            foreach (AudioSource audioSource in audioSourcesInScene)
            {
                if (audioSource.gameObject.tag != "AudioController")
                {
                    audioSource.mute = false;
                }
            }
        }

    }
    

}
