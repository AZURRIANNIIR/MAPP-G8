using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image background;
    public TMP_Text text;

    void Start()
    {
        int level = SelectLevel.selectedLevel;
        text.text = "Level " + level.ToString();
        background.sprite = backgrounds[level - 1];

    }

    public void GoBackToLevelSelection()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}