using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * When activated, it will cycle through breifly turning its illuminatable objects on and off in order.
 */
public class TimedIlluminator : MonoBehaviour {

    public float blinkDuration; //How long each blink lasts.
    public float pauseBetweenBlinks; //The pause between individual blinks.
    public float interCylcePause; //The pause between each round of blinks.

    public IlluminatingObject[] toIlluminateInOrder;
    
    public void Activate()
    {
        StartCoroutine(BlinkCycle());
    }
    
    /**
     * Repeatedly cycles, blinking each illuminated object in order.
     */ 
    private IEnumerator BlinkCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(interCylcePause);

            foreach(IlluminatingObject obj in toIlluminateInOrder)
            {
                StartCoroutine(obj.Illuminate(blinkDuration / 2));
                yield return new WaitForSeconds(blinkDuration / 2);
                StartCoroutine(obj.Deilluminate(blinkDuration / 2));
                yield return new WaitForSeconds(blinkDuration / 2);
                yield return new WaitForSeconds(pauseBetweenBlinks);
            }
        }
    }

}
