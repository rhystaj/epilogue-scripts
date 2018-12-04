using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminatingObject : MonoBehaviour {

    public Material materialWhenIllumianted; //The material the button will have when illuminated.

    private MeshRenderer renderer;
    private Material unilluminatedMaterial; //Pulled from the default given to the mesh renderer.
    private Light light; //The attached light component that will be activated when the button is pushed.
    private float lightIntensity;

    private void Start()
    {
        //Get components
        renderer = GetComponent<MeshRenderer>();
        unilluminatedMaterial = renderer.material;
        light = GetComponent<Light>();
        lightIntensity = light.intensity;

        //Initialise light
        light.enabled = false;
        light.color = materialWhenIllumianted.color; //Set colour of the light to match the material when it is illuminated.

    }

    /**
     * Have the object illuminate.
     */
    public void Illuminate()
    {
        Debug.Log(renderer);

        renderer.material = materialWhenIllumianted;
        light.enabled = true;
    }

    /**
     * Turn the illuminating off.
     */
    public void Deilluminate()
    {
        renderer.material = unilluminatedMaterial;
        light.enabled = false;
    }

    /**
     * Have the object illuminate over time.
     */
    public IEnumerator Illuminate(float seconds)
    {
        return LerpLight(unilluminatedMaterial, materialWhenIllumianted, 0, lightIntensity, seconds);   
    }

    public IEnumerator Deilluminate(float seconds)
    {
        return LerpLight(materialWhenIllumianted, unilluminatedMaterial, lightIntensity, 0, seconds);
    }

    private IEnumerator LerpLight(Material start, Material end, float intensityStart, float intensityEnd, float seconds)
    {
        Material tempMat = new Material(start); //The material used for the transition.
        float targetIntensity = intensityEnd;

        renderer.material = tempMat;
        tempMat.EnableKeyword("_EMMISSION");
        tempMat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        Color startEmmissionColour = start.GetColor("_EmissionColor");
        Color endEmmissionColour = end.GetColor("_EmissionColor");
        if (startEmmissionColour == null) startEmmissionColour = Color.black;
        if (endEmmissionColour == null) endEmmissionColour = Color.black;

        light.intensity = intensityStart;
        light.enabled = true;

        Vector2 intensityStartVector = new Vector2(intensityStart, 0);
        Vector3 intensityEndVector = new Vector2(intensityEnd, 0);

        float totalTime = 0;
        while (totalTime < seconds)
        {

            Color currentColor = Color.Lerp(start.color, end.color, totalTime / seconds);
            tempMat.color = currentColor;
            light.color = currentColor;

            Color currentEmmision = Color.Lerp(startEmmissionColour, endEmmissionColour, totalTime / seconds);
            tempMat.SetColor("_EmissionColor", currentEmmision);

            Vector2 currentIntensityVector = Vector2.Lerp(intensityStartVector, intensityEndVector, totalTime / seconds);
            light.intensity = currentIntensityVector.x;

            totalTime += Time.deltaTime;

            renderer.UpdateGIMaterials();

            yield return null;

        }

        renderer.material = end;

        yield return null;
    }

}
