using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour


{
    [SerializeField] Button[] buttons;
    
    
    void Start()
    {
        int unlockLevel = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i < unlockLevel;
        }
        //if (!PlayerPrefs.HasKey("levelsUnlocked"))
       // {
            //PlayerPrefs.SetInt("levelsUnlocked", 1);
       // }
       // unlockedLevel = PlayerPrefs.GetInt("levelsUnlocked");

       // for(int i = 0; i<buttons.Length; i++)
       // {
          //  if(i < unlockedLevel)
          //  {
            //    buttons[i].interactable = true;
           // }
           // else
           // {
           //     buttons[i].interactable = false;
           // }
      //  }
    }
}
