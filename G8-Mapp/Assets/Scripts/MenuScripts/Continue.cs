using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public void ContinueGame()
    {
        //Anropas n�r spelaren klickar p� continue-knappen, courontine g�r att de kan bli en paus innan scenen laddas
        StartCoroutine(LoadSceneDelay());
           // SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
    private IEnumerator LoadSceneDelay()
    {
        //korutinmetod som g�r att scenen laddas med 1 sekunds delay(f�r att vi vill ha knappljudet vid tryckning) Efter f�rdr�jningen kan scenen laddas in med n�sta rad
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));

    }
    private void OnApplicationFocus(bool focus)
    {
        //Om spelaren g�r ut ur spelet osv sparas det nuvarande buildIndex med playerPrefs, vilket g�r att nuvarande scen sparas var spelaren l�mnade det
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
