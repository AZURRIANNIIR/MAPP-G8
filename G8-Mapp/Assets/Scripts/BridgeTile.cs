using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Vi ärver här från GridTile-skriptet
public class BridgeTile : GridTile
{
    [SerializeField] public bool crossedOnce;
    [SerializeField] private SnakeMovement snakeMovement;

    [Header("Colliders")]
    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    [SerializeField] private GameObject temporaryCollider;

    [Header("States")]
    [SerializeField] private bool steppedOn;

    public UnityEvent OnCrossedOnceStatus;

    new private void Start()
    {
        base.Start();
        crossedOnce = false;

        if (!snakeMovement)
        {
            snakeMovement = FindObjectOfType<SnakeMovement>();
        }
    }

#if UNITY_EDITOR
    new private void Reset()
    {
        base.Reset();

        AudioClip crossedOnceSound = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Sounds/crossroad_crossed_once_sound.wav", typeof(AudioClip));
        if (!crossedOnceSound)
        {
            Debug.LogError(transform.parent.gameObject.name +": Kunde inte hitta det ljudklipp som är specifierat för första korsningen i skript-filen.");
            return;
        }

        UnityAction<AudioClip> playCrossedOnceSound = new UnityAction<AudioClip>(GameObject.Find("SFX_Object").GetComponent<AudioSource>().PlayOneShot);
        if (playCrossedOnceSound == null)
        {
            Debug.LogError(transform.parent.gameObject.name = ": Kunde inte hitta ett AudioController-objekt. Finns det ett i scenen?");
            return;
        }
        UnityEditor.Events.UnityEventTools.AddObjectPersistentListener(OnCrossedOnceStatus, playCrossedOnceSound, crossedOnceSound);

        UnityAction<float> increasePitch = new UnityAction<float>(GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerScript>().IncreasePitchForSFX);
        UnityEditor.Events.UnityEventTools.AddFloatPersistentListener(OnCrossedOnceStatus, increasePitch, PitchIncreaseValue);
    }
#endif

    private void Update()
    {
        if (steppedOn)
        {
            if (snakeMovement.bridgeDisabled)
            {
                temporaryCollider.GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (!snakeMovement.bridgeDisabled)
            {
                temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
                steppedOn = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!UndoButton.EventFired)
        {
            //Om vi redan har korsat bron en gång
            if (collision.gameObject.CompareTag("Snake") && crossedOnce && !GetTakenStatus())
            {
                SetTakenStatus(true);
                print("ny plats");
                tileCollider.TakeTile();
                gameController.tileTaken();
                OnTakenStatus?.Invoke();
                return;
            }

            //Om bron korsas för första gången
            else if (collision.gameObject.CompareTag("Snake") && !crossedOnce && !GetTakenStatus())
            {
                //Typkonvertera för att se till så att vi kan använda korsningsfunktioner
                if (tileCollider.GetType() == typeof(BridgeColliderScript))
                {
                    BridgeColliderScript bridgeCollider = (BridgeColliderScript)tileCollider;
                    bridgeCollider.ChangeBridgeSpriteToTaken();
                }
                crossedOnce = true;
                OnCrossedOnceStatus?.Invoke();
                gameController.tileTaken();

                print("bridge taken once");
            }
        }
    }

    new private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Snake"))
        {
            steppedOn = true;
            turnOnPath();
        }

        if (UndoButton.EventFired)
        {
            steppedOn = false;
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
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

    private void turnOnPath()
    {
        print("Colliders borde stängas av");
        upperBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        lowerBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        leftBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
        rightBoxCollider.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void turnOffPath(string direction)
    {
        print("Colliders borde sättas på");
        if (direction == "Horizontal"){
            upperBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
            lowerBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (direction == "Vertical")
        {
            leftBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
            rightBoxCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
