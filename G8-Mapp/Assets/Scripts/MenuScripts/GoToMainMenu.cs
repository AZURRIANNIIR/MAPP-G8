using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMainMenu : MonoBehaviour
{

    public GameObject mainMenuButton;

    public void goBacktoMainMenu () {
        SceneManager.LoadScene(0);
    }


    
}
