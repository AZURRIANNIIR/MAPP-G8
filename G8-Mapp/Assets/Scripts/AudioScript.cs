using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    private static AudioScript instance;
    


     void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName != "MainMenu")
        {
            if (instance != null)
            {
                Destroy(gameObject);

            }
            else
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);

            }
        }
        
        
        
    }
}
