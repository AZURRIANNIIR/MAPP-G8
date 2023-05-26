using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    //Används i grid för LevelSelect för att låsa upp banor 
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
        //En sekunds delay när spelaren har klickat på knappen för att man ska höra knappljudet
        selectedLevel = level;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level.ToString());
    }
}
