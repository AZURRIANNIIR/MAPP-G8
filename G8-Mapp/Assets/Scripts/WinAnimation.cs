using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimation : MonoBehaviour
{
    
 [SerializeField] private GameController gameController;
    private SpriteRenderer snakeSprite;
    [SerializeField] private Animator panelAnimator;



    // Start is called before the first frame update
    
    
        
    

    // Update is called once per frame
    void Update()
    {
        // if(snakePrefab.transform.position == goalPrefab.transform.position && gameController.GameWon == true){
            //trigga animationen
            //animator.SetTrigger(SnakeWinAnimation);
            if(gameController.GameWon == true){
                snakeSprite.enabled = true;
                panelAnimator.SetTrigger("StartAnimation");
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        snakeSprite = GetComponent<SpriteRenderer>();
        snakeSprite.enabled = false;
        
    }

    // Update is called once per frame

}
