using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    
    
    void Start()
    {
        //Anv�nder sig av playerprefs f�r att l�sa upp en level
        int unlockLevel = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = + i < unlockLevel;
        }
    }
}
