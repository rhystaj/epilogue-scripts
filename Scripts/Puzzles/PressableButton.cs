using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour {

    public Vector3 pressDistance;
    public float pressSpeed;

    private bool locked;


    public IEnumerator PressAndLock(Action onCompletion)
    {
        if (!locked)
        {
			GetComponent<AudioSource> ().Play ();

            locked = true;
            StartCoroutine(OverTime.Translate(transform, pressDistance, pressSpeed, null));
            yield return new WaitForSeconds(pressSpeed);
        }

        if (onCompletion != null) onCompletion();
    }

    public IEnumerator UnlockAndRelease(Action onCompletion)
    {
        if (locked) {

			GetComponent<AudioSource> ().Play ();

            StartCoroutine(OverTime.Translate(transform, pressDistance * -1, pressSpeed, null));
            yield return new WaitForSeconds(pressSpeed);
            locked = false;
        }

        if (onCompletion != null) onCompletion();
    }

    public IEnumerator PressAndRelease(Action onCompletion)
    {
        if (!locked)
        {
			GetComponent<AudioSource> ().Play ();

			locked = true;
            StartCoroutine(OverTime.Translate(transform, pressDistance, pressSpeed, null));
            yield return new WaitForSeconds(pressSpeed);
            StartCoroutine(OverTime.Translate(transform, pressDistance * -1, pressSpeed, null));
            yield return new WaitForSeconds(pressSpeed);
            locked = false;
        }

        if (onCompletion != null) onCompletion();
    }

    /**
     * For saving. Allows the manager using this button to lock it instantly to keep the world state consistent.
     */ 
    public void Lock(){

        if (locked) return;

        locked = true;
        transform.Translate(pressDistance);
    }

    /**
     * For saving. Allows the manager using this button to unlock it instantly to keep the world state consistent.
     */
    public void Unlock()
    {
        if (!locked) return;

        locked = false;
        transform.Translate(pressDistance * -1);
    }

    /*
    private IEnumerator PressButton()
    {
        float totalTime = 0;
        while (totalTime < pressSpeed)
        {
            float timeSince = Time.deltaTime;
            transform.Translate(
                0, 0, pressDistance * (timeSince / pressSpeed));
            totalTime += timeSince;
            yield return null;
        }
    }

    private IEnumerator ReleaseButton()
    {
        float totalTime = 0;
        while (totalTime < pressSpeed)
        {
            float timeSince = Time.deltaTime;
            transform.Translate(0, 0, pressDistance * (timeSince / pressSpeed) * -1);
            totalTime += timeSince;
            yield return null;
        }
    }
    */
}
