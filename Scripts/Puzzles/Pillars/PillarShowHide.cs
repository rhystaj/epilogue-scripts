using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A component that reveals a pillar by raising it from the ground and hides it by lowering it back.
 */ 
public class PillarShowHide : MonoBehaviour {

    public bool hidden; //Whether the pillar is currently visible.
    public float duration;
    public float hiddenYDifference; //Difference the difference in the y value from the pillar's position when it is hidden.

    private RotatingPillar rotatingPillar;

    private void Start()
    {
        if (hidden) transform.Translate(0, hiddenYDifference, 0);

        if(rotatingPillar != null) rotatingPillar.enabled = false;
        rotatingPillar = GetComponent<RotatingPillar>();
    }

    /**
     * Lower and hide the pillar.
     */
    public void Hide()
    {
        if (hidden) return; //No point in hiding a pillar that is already hidden.

        StartCoroutine(OverTime.Translate(transform, new Vector3(0, hiddenYDifference, 0), duration, AfterAnimation));
    }

    /**
     * Raise and show the pillar.
     */ 
    public void Show()
    {
        if (!hidden) return; //No point in showing a pillar that is already being shown.

        StartCoroutine(OverTime.Translate(transform, new Vector3(0, hiddenYDifference * -1, 0), duration, AfterAnimation));
    }

    private void AfterAnimation()
    {
        if(rotatingPillar != null) rotatingPillar.enabled = true;
        hidden = !hidden;
    }
	
}
