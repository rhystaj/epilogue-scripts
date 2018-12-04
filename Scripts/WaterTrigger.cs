using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour {

    public Transform waterTransform;
    public float heightChange;
    public float duration;
    public ProgressionManager manager;
    public AudioSource ambient;
    public AudioSource music;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Debug.Log("Raise");

        StartCoroutine(OverTime.Translate(waterTransform, new Vector3(0, heightChange, 0), duration, AfterRise));
        triggered = true;
    }

    private void AfterRise()
    {

        ambient.Pause();
        music.Pause();

        manager.TransitionScenes(true);

    }
}
