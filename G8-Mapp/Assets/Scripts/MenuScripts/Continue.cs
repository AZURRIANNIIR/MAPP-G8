using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    private int sceneToContinue;

    private void Start()
    {
        Debug.Log(sceneToContinue);
    }

    public void ContinueGame()
    {

        //if (sceneToContinue != 0)
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        //else
        //    return;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
        else
        {
            PlayerPrefs.SetInt("SavedScene",SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void saveScene()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    }
}
