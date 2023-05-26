using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    
    
    void Start()
    {
        //Använder sig av playerprefs för att låsa upp en level
        int unlockLevel = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = + i < unlockLevel;
        }
    }
}
