using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public void ContinueGame()
    {
        //anv�nder sig av playerprefs som finns i unlocklevel f�r att kolla om en level �r uppl�st och laddar in den senast uppl�sta leveln p� continue knappen
        int savedScene = PlayerPrefs.GetInt("levelsUnlocked");
        if(savedScene > 0)
        {
            StartCoroutine(LoadSceneDelay(savedScene));
        }
        else
        {
            SceneManager.LoadScene("1");
        }
        //Anropas n�r spelaren klickar p� continue-knappen, courontine g�r att de kan bli en paus innan scenen laddas
           
    }
    private IEnumerator LoadSceneDelay(int scene)
    {
        //korutinmetod som g�r att scenen laddas med 1 sekunds delay(f�r att vi vill ha knappljudet vid tryckning) Efter f�rdr�jningen kan scenen laddas in med n�sta rad
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
        Debug.Log(PlayerPrefs.GetInt("levelsUnlocked"));

    }
}




