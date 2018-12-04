using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDelete : MonoBehaviour {

    public float duration; //The duration of the fade.

    public void Delete()
    {
        StartCoroutine(FadeAndDelete());
    }

    /**
     * Have the object fade out and then delete object from scene.
     * IMPORTANT: Render mode of material of object MUST be set to "Fade" for the effect to work correctly.
     */ 
    private IEnumerator FadeAndDelete()
    {

        Material material = GetComponent<MeshRenderer>().material;

        Color startColour = material.color;
        Color endColor = new Color(startColour.g, startColour.g, startColour.b, 0);

        float totalTime = 0;
        while(totalTime < duration)
        {
            float timeSince = Time.deltaTime;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1 - totalTime / duration);

            totalTime += timeSince;
            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }

	
}
