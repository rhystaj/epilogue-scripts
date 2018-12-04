using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour {

	private void OnMouseDown(){
		Debug.Log ("Play");

		AudioSource source = GetComponent<AudioSource> ();

		if(!source.isPlaying)
			source.Play ();
	}
}
