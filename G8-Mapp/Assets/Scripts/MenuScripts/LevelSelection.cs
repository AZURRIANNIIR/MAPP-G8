using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private string levelName;

    private IEnumerator WaitForSceneLoad(string LevelName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelName);
    }
    public void LoadSceneDelay(string levelName)
    {
        StartCoroutine(WaitForSceneLoad(levelName));
    }
}

    