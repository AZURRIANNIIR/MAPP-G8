using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    public static int selectedLevel;
    public int level;
    public TMP_Text text;
    void Start()
    {
        text.text = level.ToString();
    }

  public void OpenScene()
    {
        selectedLevel = level;
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
