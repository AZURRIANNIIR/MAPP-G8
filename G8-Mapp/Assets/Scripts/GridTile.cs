using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridTile : MonoBehaviour
{
    [SerializeField] private bool taken = false;
    [SerializeField] protected ColliderScript tileCollider;
    [SerializeField] protected GameController gameController;
    public UnityEvent OnTakenStatus;

    readonly protected float PitchIncreaseValue = 0.02f;

   // private AudioClip tileTakenSound;

    protected void Start()
    {
        if (!tileCollider)
        {
            tileCollider = GetComponentInParent<ColliderScript>();
        }

        if (!gameController)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

#if UNITY_EDITOR
    protected void Reset()
    {
        Debug.Log(gameObject.name + " kör Reset nu.");
        AudioClip tileTakenSound = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Sounds/tile_taken_sound.wav", typeof(AudioClip));

        UnityAction<AudioClip> playSound = new UnityAction<AudioClip>(GameObject.Find("SFX_Object").GetComponent<AudioSource>().PlayOneShot);
        UnityEditor.Events.UnityEventTools.AddObjectPersistentListener(OnTakenStatus, playSound, tileTakenSound);

        UnityAction<float> increasePitch = new UnityAction<float>(GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerScript>().IncreasePitchForSFX);
        UnityEditor.Events.UnityEventTools.AddFloatPersistentListener(OnTakenStatus, increasePitch, PitchIncreaseValue);
    }
#endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && !taken && !UndoButton.EventFired)
        {
            SetTileAsTaken();
        }
    }

    private void SetTileAsTaken()
    {
        taken = true;
        print("ny plats");
        tileCollider.TakeTile();
        gameController.tileTaken();
        OnTakenStatus.Invoke();
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snake") && taken)
        {
            tileCollider.EnableCollider();
        }
    }

    public void SetTakenStatus(bool state)
    {
        taken = state;
    }

    public bool GetTakenStatus() { return taken; }
}
