using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameController gameController;
    void Start()
    {
        
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

        if (gameController.win == true)
        {
            em.enabled = true;
        }
        
    }
}
