using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public void ContinueGame()
    {
        //använder sig av playerprefs som finns i unlocklevel för att kolla om en level är upplåst och laddar in den senast upplåsta leveln på continue knappen
        int savedScene = PlayerPrefs.GetInt("levelsUnlocked");
        if(savedScene > 0)
        {
            StartCoroutine(LoadSceneDelay(savedScene));
        }
        else
        {
            SceneManager.LoadScene("1");
        }
        //Anropas när spelaren klickar på continue-knappen, courontine gör att de kan bli en paus innan scenen laddas
           
    }
    private IEnumerator LoadSceneDelay(int scene)
    {
        //korutinmetod som gör att scenen laddas med 1 sekunds delay(för att vi vill ha knappljudet vid tryckning) Efter fördröjningen kan scenen laddas in med nästa rad
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
        Debug.Log(PlayerPrefs.GetInt("levelsUnlocked"));

    }
}




