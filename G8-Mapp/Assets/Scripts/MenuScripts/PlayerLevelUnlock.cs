using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUnlock : MonoBehaviour
{
    [SerializeField] int levelUnlock = 0;
    int numberLocked;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Kollar om de har kollidat med tagen snake och om den har det så låser den upp leveln
        if (other.gameObject.CompareTag("Snake"))
        {
            numberLocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

            if (levelUnlock > numberLocked)
            {
                PlayerPrefs.SetInt("levelsUnlocked", levelUnlock);
                numberLocked = levelUnlock;
            }      
            Debug.Log("Upplåst: " + numberLocked + "levelUnlock: " + levelUnlock);

        }
        
    }

}
