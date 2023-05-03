using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    int unlockedLevel;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        unlockedLevel = PlayerPrefs.GetInt("levelsUnlocked");

        for(int i = 0; i<buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    void Update()
    {
        unlockedLevel = PlayerPrefs.GetInt("levelsUnlocked");

        for(int i = 0; i<unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
        
    }
}
