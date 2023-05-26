using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] public GameController gameController;
    void Start()
    {
        if (!gameController)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystemOn();
    }

    public void ParticleSystemOn()
    {

        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;

        if (gameController.GameWon == true)
        {
            em.enabled = true;
        }
        
    }
}
