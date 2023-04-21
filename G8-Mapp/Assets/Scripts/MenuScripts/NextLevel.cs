using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public Button nextLevel;
    public string nameLevel;
    private bool levelCompleted;

    private void Start()
    {
        // måste finnas en knapp som heter LoadNextLevel där spelaren klickar för att aktivera, kan bara klickas på om spelaren har klarat leveln?
        nextLevel.onClick.AddListener(LoadNextLevel);
        nextLevel.interactable = false;
    }
    
    public void LevelCompleted()
    {
        //Om banan är completed kommer man kunna trycka på knappen
        levelCompleted = true;
        nextLevel.interactable = true;
    }
    void LoadNextLevel()
        //har laddar den in nästa scen för spelaren genom att man kunner kunna skriva in namnet på banan i OnClick()?
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameLevel);
    }
    private void Update()
    {
        //Om banan inte är completed så kan man inte trycka på knappen?
        if (!levelCompleted)
        {
            nextLevel.interactable = false;
        }
    }
    //Vet inte om detta är en bra lösning eller om de ens fungerar men kanske??
}
