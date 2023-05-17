using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    public int unlockLevel = 1;
    
    
    void Start()
    {
       
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        unlockLevel = PlayerPrefs.GetInt("levelsUnlocked");

        for(int i = 0; i<buttons.Length; i++)
        {
            if(i < unlockLevel)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
        Debug.Log("LevelUnlocked: "+ unlockLevel);
    }
}
