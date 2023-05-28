using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject panelOne;
    [SerializeField] private GameObject panelTwo;
    [SerializeField] private GameObject panelThree;
    static int pressAmount;
    [SerializeField] private int panelChange;
    [SerializeField] private Button button;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    //private Animator panelOneAnimator;
    //private Animator panelTwoAnimator;




    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        ChangePanel();
        //panelOneAnimator = panelOne.GetComponent<Animator>();
        //panelTwoAnimator = panelTwo.GetComponent<Animator>();

    }

    void TaskOnClick()
    {
        pressAmount += panelChange;
        print(pressAmount);
        //PlayAnimation();
        ChangePanel();
        //checkButtonEnabled();
        if (PlayerPrefs.GetInt("SOUNDFX_MUTED") == 0) {
            audioSource.PlayOneShot(audioClip);
        }

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
            panelOne.SetActive(true);
            panelTwo.SetActive(false);
            panelThree.SetActive(false);
        }
        else if (pressAmount == 1)
        {
            panelOne.SetActive(false);
            panelTwo.SetActive(true);
            panelThree.SetActive(false);
        }
        else if (pressAmount == 2)
        {
            panelOne.SetActive(false);
            panelTwo.SetActive(false);
            panelThree.SetActive(true);
        }
    }

    //private void PlayAnimation()
    //{
    //    if (pressAmount == 0)
    //    {
    //        panelOneAnimator.SetInteger("panelSwitch", 1);
    //    }
    //    else if (pressAmount == 1 && panelChange == 1)
    //    {
    //        panelOneAnimator.SetInteger("panelSwitch", 2);
    //    }
    //    else if (pressAmount == 1 && panelChange == -1)
    //    {
    //        panelOneAnimator.SetInteger("panelSwitch", 4);
    //    }
    //    else if (pressAmount == 2)
    //    {
    //        panelOneAnimator.SetInteger("panelSwitch", 3);
    //    }
    //}


}
