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
        //ser till att knappen �r disabled fr�n b�rjan
        nextLevel.interactable = false;
        text.enabled = false;

        nextLevel.onClick.AddListener(GetToNextLevel);
    }
    private void Update()
    {
        //Kollar om snake har n�tt goalprefab
        if(snakePrefab.transform.position == goalPrefab.transform.position)
        {
           //G�r att knappen �r p�slagen n�r de har n�tt goalprefab
            nextLevel.interactable = true;
            text.enabled = true;
        }
    }
    private void GetToNextLevel()
    {
        //Laddar in n�sta scen
        SceneManager.LoadScene(nextScene);  
    }
}
