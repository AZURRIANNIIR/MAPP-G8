using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class ScrollLevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button buttonOne;
    [SerializeField] private Button buttonTwo;
    private int panelIndex = 0;

    private void Start()
    {
        buttonOne.onClick.AddListener(OtherPanel);
        buttonTwo.onClick.AddListener(OtherPanelAgain);
        UpdatingButtons();
        NewPanel(panelIndex);
    }
    private void OtherPanel()
    {
        NewPanel(panelIndex - 1);
    }
    private void OtherPanelAgain()
    {
        NewPanel(panelIndex + 1);
    }
    private void NewPanel(int index)
    {
        if(index >= 0 && index < panels.Length)
        {
            panels[panelIndex].SetActive(false);
            panelIndex = index;
            panels[panelIndex].SetActive(true);
            UpdatingButtons();
        }
    }
    private void UpdatingButtons()
    {
        buttonOne.interactable = (panelIndex > 0);
        buttonTwo.interactable= (panelIndex < panels.Length - 1);
    }

}


