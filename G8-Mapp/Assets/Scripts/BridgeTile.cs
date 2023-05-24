using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BridgeTile : GridTile
{
    [SerializeField] private bool crossedOnce;
    [SerializeField] private SnakeMovement snakeMovement;
    [SerializeField] private GameObject snake;

    [Header("Colliders")]
    [SerializeField] private GameObject upperBoxCollider;
    [SerializeField] private GameObject lowerBoxCollider;
    [SerializeField] private GameObject leftBoxCollider;
    [SerializeField] private GameObject rightBoxCollider;

    [SerializeField] private GameObject temporaryCollider;

    [Header("States")]
    [SerializeField] string enterDirection;

    public UnityEvent OnCrossedOnceStatus;

    private GridTile afterGrid;

    new private void Start()
    {
        base.Start();
        crossedOnce = false;

        if (!snakeMovement)
        {
            snakeMovement = FindObjectOfType<SnakeMovement>();
        }

        if (!snake)
        {
            snake = GameObject.FindGameObjectWithTag("Snake");
        }
    }
#if UNITY_EDITOR
    new private void Reset()
    {
        base.Reset();

        AudioClip crossedOnceSound = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Sounds/crossroad_crossed_once_sound.wav", typeof(AudioClip));

        UnityAction<AudioClip> playCrossedOnceSound = new UnityAction<AudioClip>(GameObject.Find("SFX_Object").GetComponent<AudioSource>().PlayOneShot);
        UnityEditor.Events.UnityEventTools.AddObjectPersistentListener(OnCrossedOnceStatus, playCrossedOnceSound, crossedOnceSound);

        UnityAction<float> increasePitch = new UnityAction<float>(GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerScript>().IncreasePitchForSFX);
        UnityEditor.Events.UnityEventTools.AddFloatPersistentListener(OnCrossedOnceStatus, increasePitch, PitchIncreaseValue);
    }
#endif

    private void Update()
    {
        if (snake.transform.position.x == transform.position.x && (snake.transform.position.y == transform.position.y+1 || snake.transform.position.y == transform.position.y-1) && enterDirection.Equals("Vertical") && crossedOnce)
        {
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (snake.transform.position.y == transform.position.y && (snake.transform.position.x == transform.position.x + 1 || snake.transform.position.x == transform.position.x - 1) && enterDirection.Equals("Horizontal") && crossedOnce)
        {
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
        else 
        {
            temporaryCollider.GetComponent<BoxCollider2D>().enabled = false;
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
            }
        }
    }

    new private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Snake"))
        {
            turnOnPath();
        }

        if (collision.gameObject.CompareTag("Snake") && GetTakenStatus())
        {
            tileCollider.EnableCollider();
        }

        if (collision.gameObject.CompareTag("Snake") && UndoButton.EventFired)
        {
            tileCollider.DisableCollider();
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

    public void SetTileAfterBridge(GridTile tileScript)
    {
        if (tileScript == null)
        {
            return;
        }

        afterGrid = tileScript;
        afterGrid.SnakeOnTile += SetTemporaryColliderStatus;
    }

    private void ClearTileAfterBridge()
    {
        afterGrid = null;
    }

    private void SetTemporaryColliderStatus(bool state)
    {
        temporaryCollider.GetComponent<Collider2D>().enabled = state;
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

        if (!crossedOnce)
        {
            enterDirection = direction;
        }
    }

    private void OnEnable()
    {
        SnakeMovement.OnReturnToStart += ClearTileAfterBridge;
    }

    private void OnDisable()
    {
        if (afterGrid)
        {
            afterGrid.SnakeOnTile -= SetTemporaryColliderStatus;
        }
        SnakeMovement.OnReturnToStart -= ClearTileAfterBridge;


    }
}
