using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    public int unlockedLevel = 1;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            //PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        unlockedLevel = PlayerPrefs.GetInt("levelsUnlocked");

        for(int i = 0; i<buttons.Length; i++)
        {
            if(i < unlockedLevel)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}
