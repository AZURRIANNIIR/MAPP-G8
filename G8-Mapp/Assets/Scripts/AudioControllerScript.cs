using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControllerScript : MonoBehaviour
{
    private const float PITCH_CHANGE_VALUE = 0.02f;
    private const float PITCH_MIN_VALUE = 1.0f;

    [SerializeField] private AudioSource sfxSource, musicSource;

    [SerializeField] private bool respectUserSoundSettings;

    private void Awake()
    {
        //Subscriba till Undo-knappens event automatiskt, s� slipper vi g�ra det manuellt f�r varje scen.
        FindObjectOfType<UndoButton>().GetComponent<Button>().onClick.AddListener(() => DecreasePitchForSFX(PITCH_CHANGE_VALUE));
        AudioSource[] sources = { sfxSource, musicSource };

        if (respectUserSoundSettings) 
        {
            foreach (AudioSource source in sources)
            {
                source.volume = AudioListener.volume;
            }
        }
    }

    private void Update()
    {
        //Pitchen f�r v�ra AudioSources ska inte bli mindre �n standardv�rdet (Som best�ms av v�ran konstant)
        sfxSource.pitch = Mathf.Clamp(sfxSource.pitch,PITCH_MIN_VALUE, Mathf.Infinity);
        musicSource.pitch = Mathf.Clamp(musicSource.pitch,PITCH_MIN_VALUE, Mathf.Infinity);
    }

    #region Funktioner f�r pitchen (endast f�r ljudeffekter)
    public void IncreasePitchForSFX(float pitchValue)
    {
        sfxSource.pitch += pitchValue;
    }

    public void DecreasePitchForSFX(float pitchValue)
    {
        sfxSource.pitch -= pitchValue;    
    }

    public void PlaySFXWithTempPitch(float pitchValue)
    {
        float tempPitch = sfxSource.pitch;
        sfxSource.pitch = pitchValue;
        sfxSource.PlayOneShot(sfxSource.clip);
        //Coroutinen m�jligg�r en f�rdr�jning
        StartCoroutine(ResetPitch(tempPitch));
    }
    #endregion

    private IEnumerator ResetPitch(float pitch)
    {
        yield return new WaitForSeconds(sfxSource.clip.length);
        sfxSource.pitch = pitch;
    }
}
