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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button.onClick.Invoke();
    }



}
