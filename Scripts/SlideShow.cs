using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShow : MonoBehaviour {

    public Material[] materials;
    public float slideLength;
    public MeshRenderer renderer;
    private int slide = 0;


    public void StartSlideShow()
    {
        StartCoroutine(PlaySlideShow());
    }

    private IEnumerator PlaySlideShow()
    {
        while (true)
        {
            renderer.material = materials[slide];
            yield return new WaitForSeconds(slideLength);
            slide = (slide + 1) % materials.Length;
        }
    }

}
