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
        // m�ste finnas en knapp som heter LoadNextLevel d�r spelaren klickar f�r att aktivera, kan bara klickas p� om spelaren har klarat leveln?
        nextLevel.onClick.AddListener(LoadNextLevel);
        nextLevel.interactable = false;
    }
    
    public void LevelCompleted()
    {
        //Om banan �r completed kommer man kunna trycka p� knappen
        levelCompleted = true;
        nextLevel.interactable = true;
    }
    void LoadNextLevel()
        //har laddar den in n�sta scen f�r spelaren genom att man kunner kunna skriva in namnet p� banan i OnClick()?
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameLevel);
    }
    private void Update()
    {
        //Om banan inte �r completed s� kan man inte trycka p� knappen?
        if (!levelCompleted)
        {
            nextLevel.interactable = false;
        }
    }
    //Vet inte om detta �r en bra l�sning eller om de ens fungerar men kanske??
}
