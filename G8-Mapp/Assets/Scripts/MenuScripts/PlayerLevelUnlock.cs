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
        if (other.gameObject.CompareTag("Snake"))
        {
            Debug.Log("Current levelUnlocked: "+ numberLocked);
            Debug.Log("något pls funka: "+ levelUnlock);
            if(levelUnlock > PlayerPrefs.GetInt("levelsUnlocked"))
            {
                if (levelUnlock > numberLocked)
                {
                    PlayerPrefs.SetInt("levelsUnlocked", levelUnlock);
                }
            }
        }
    }

}
