using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A rock that falls and changes its appearance when activated.
 */
public class RewardRock : Outcome {

    public Material materialChange;
	public OutcomeProgress progress;

    private Light light;
    private MeshRenderer renderer;

	// Use this for initialization
	void Start () {

        light = GetComponent<Light>();
        light.enabled = false;

        renderer = GetComponent<MeshRenderer>();
	}
	
    public override void Activate()
    {
        renderer.material = materialChange;
        light.enabled = true;

		GetComponent<AudioSource> ().Play ();

		progress.UpdateProgress (this);
    }
		
}
