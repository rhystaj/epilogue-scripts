using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrigger : MonoBehaviour {

    public AudioClip build;
    public AudioSource music;

    private void OnTriggerEnter(Collider other)
    {
        music.clip = build;
        music.volume = 1f;
        music.Play();
    }
}
