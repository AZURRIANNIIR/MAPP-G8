using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections;

public class ChooseLanguage : MonoBehaviour
{

    private bool active = false;

    public void ChangeLanguage (int languageID){

    if(active == true)
    return;
    StartCoroutine(setLanguage(languageID));

   }

  IEnumerator setLanguage(int languageID){
    active=true;
    yield return LocalizationSettings.InitializationOperation;
    LocalizationSettings.SelectedLocale=LocalizationSettings.AvailableLocales.Locales[languageID];
    active=false;
  }
}


