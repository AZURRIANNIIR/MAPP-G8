using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public void ContinueGame()
    {
        //Anropas när spelaren klickar på continue-knappen, courontine gör att de kan bli en paus innan scenen laddas
        StartCoroutine(LoadSceneDelay());
           // SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
    private IEnumerator LoadSceneDelay()
    {
        //korutinmetod som gör att scenen laddas med 1 sekunds delay(för att vi vill ha knappljudet vid tryckning) Efter fördröjningen kan scenen laddas in med nästa rad
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));

    }
    private void OnApplicationFocus(bool focus)
    {
        //Om spelaren går ut ur spelet osv sparas det nuvarande buildIndex med playerPrefs, vilket gör att nuvarande scen sparas var spelaren lämnade det
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
        //Denna metod sparar den nuvarande scenen
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    }
}
