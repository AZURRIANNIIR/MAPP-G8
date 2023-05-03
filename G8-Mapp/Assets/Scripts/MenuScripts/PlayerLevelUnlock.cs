using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUnlock : MonoBehaviour
{
    public int levelUnlock;
    int numberLocked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            numberLocked = PlayerPrefs.GetInt("levelsUnlocked");

            if(numberLocked <= levelUnlock)
            {
                PlayerPrefs.SetInt("levelsUnlocked", numberLocked +1);
                
            }

        }
    }

}
