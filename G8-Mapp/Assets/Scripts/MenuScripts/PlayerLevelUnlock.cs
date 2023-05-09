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

            PlayerPrefs.SetInt("levelsUnlocked", levelUnlock);
           // numberLocked = PlayerPrefs.GetInt("levelsUnlocked");

            //if(numberLocked <= levelUnlock)
           // {
             //   PlayerPrefs.SetInt("levelsUnlocked", numberLocked +1);
                
            //}

        }
    }

}
