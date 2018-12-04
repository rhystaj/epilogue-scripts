using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioPlayer : MonoBehaviour {

    public AudioClip clip;

	// Use this for initialization
	void Start () {
        
	}

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
