using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
   public void ClickToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ClickToQuit()
    {
        Application.Quit();
    }
    public void ClickToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
