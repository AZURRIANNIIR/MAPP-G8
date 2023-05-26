using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    //Anv�nds i grid f�r LevelSelect f�r att l�sa upp banor 
    public static int selectedLevel;
    public int level;
    public TMP_Text text;
    void Start()
    {
        text.text = level.ToString();
    }

    public void SceneOpen()
    {
        StartCoroutine(OpenSceneDelay());
    }
  public IEnumerator OpenSceneDelay()
    {
        //En sekunds delay n�r spelaren har klickat p� knappen f�r att man ska h�ra knappljudet
        selectedLevel = level;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level.ToString());
    }
}
