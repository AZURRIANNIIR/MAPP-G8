using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelScript : MonoBehaviour
{
    [SerializeField] private Image panelOne;
    [SerializeField] private Image panelTwo;
    [SerializeField] private Image panelThree;
    [SerializeField] static int pressAmount;
    [SerializeField] private int panelChange;
    [SerializeField] private Button button;



    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        ChangePanel();
    }

    void TaskOnClick()
    {
        pressAmount += panelChange;
        ChangePanel();
        checkButtonEnabled();
    }


    // Update is called once per frame
    void Update()
    {
        if (panelChange == 1)
        {
            if (pressAmount == 2)
            {
                button.enabled = false;
            }
            else
            {
                button.enabled = true;
            }
        }

        if (panelChange == -1)
        {
            if (pressAmount == 0)
            {
                button.enabled = false;
            }
            else
            {
                button.enabled = true;
            }
        }
    }

    private void ChangePanel()
    {
        if (pressAmount == 0)
        {
            panelOne.enabled = true;
            panelTwo.enabled = false;
            panelThree.enabled = false;
        } else if (pressAmount == 1)
        {
            panelOne.enabled = false;
            panelTwo.enabled = true;
            panelThree.enabled = false;
        } else if (pressAmount == 2)
        {
            panelOne.enabled = false;
            panelTwo.enabled = false;
            panelThree.enabled = true;
        }
    }

    private void checkButtonEnabled()
    {

    }


}
