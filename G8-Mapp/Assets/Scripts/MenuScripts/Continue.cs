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

    public IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
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
