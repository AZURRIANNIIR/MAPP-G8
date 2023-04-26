using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevel : MonoBehaviour
{
    public GameObject snakePrefab;
    public GameObject goalPrefab;
    public Button nextLevel;
    public string nextScene;

    public TextMeshProUGUI text;

    private void Start()
    {
        //ser till att knappen är disabled från början
        nextLevel.interactable = false;
        text.enabled = false;

        nextLevel.onClick.AddListener(GetToNextLevel);
    }
    private void Update()
    {
        //Kollar om snake har nått goalprefab
        if(snakePrefab.transform.position == goalPrefab.transform.position)
        {
           //Gör att knappen är påslagen när de har nått goalprefab
            nextLevel.interactable = true;
            text.enabled = true;
        }
    }
    private void GetToNextLevel()
    {
        //Laddar in nästa scen
        SceneManager.LoadScene(nextScene);  
    }
}
