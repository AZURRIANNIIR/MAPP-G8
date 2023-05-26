using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private IEnumerator WaitForSceneLoad(string LevelName)
    {
        //laddar in en scen med en sekunds delay genom att ta en string 
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelName);
    }
    public void LoadSceneDelay(string levelName)
    {
        StartCoroutine(WaitForSceneLoad(levelName));
    }
}

    