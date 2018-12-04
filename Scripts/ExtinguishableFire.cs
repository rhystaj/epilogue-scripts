using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishableFire : MonoBehaviour {

    public ParticleSystem fireParticles;

    public void Extinguish()
    {
        fireParticles.Stop(true);
    } 

}
