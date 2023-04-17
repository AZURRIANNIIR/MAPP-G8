using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevelSelect : MonoBehaviour
{
    public void ClickToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ClickToQuit()
    {
        Application.Quit();
    }
    public void ClickToLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
