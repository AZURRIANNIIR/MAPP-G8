using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image background;
    public Text text;

    void Start()
    {
        int level = SelectLevel.selectedLevel;
        Debug.Log(level);
        text.text = "Level " + level.ToString();
        background.sprite = backgrounds[level - 1];

    }

    public void GoBackToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelecion");
    }
}
