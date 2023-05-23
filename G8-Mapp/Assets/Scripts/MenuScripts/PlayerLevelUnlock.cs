using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUnlock : MonoBehaviour
{
    [SerializeField] int levelUnlock = 0;
    [SerializeField] bool reset = true;
    int numberLocked;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            numberLocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

            if (reset)
            {
                PlayerPrefs.DeleteAll();
            }

            if (levelUnlock > numberLocked)
            {
                PlayerPrefs.SetInt("levelsUnlocked", levelUnlock);
                numberLocked = levelUnlock;
            }      
            Debug.Log("Upplåst: " + numberLocked + "levelUnlock: " + levelUnlock);

        }
        
    }

}
