using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPillar : MonoBehaviour {

    public MultiStagePillarPuzzleManager manager;
    public int statesOfRotation; //How many individual rotations make up a complete 180 turn.
    public int sidesOfShape; //The sides in the shape of the pillar
    public float rotationDuration;
    public int[] targetRelativeRotationsAtStages; //The target rotations for each of the stages the pillar is used in. -1 for stages it isn't used in.

    private int currentRotation;

	public bool rotating;

    private void Start()
    {
        if(manager != null) manager.Subscribe(this);
    }

    /**
     * Gets the relative rotation based on the sides of the shape.
     */
    public bool isAtTargetRotation(int stage)
    {
		if(targetRelativeRotationsAtStages[stage] < 0) return true;
        return currentRotation % (statesOfRotation / sidesOfShape) == targetRelativeRotationsAtStages[stage];
    }

    public int GetCurrentRelativeRotation()
    {
        return currentRotation % (statesOfRotation / sidesOfShape);
    }

    private void OnMouseDown()
    {
		if (rotating)
			return;

		rotating = true;
		GetComponent<AudioSource> ().Play ();

        StartCoroutine(OverTime.Rotate(transform, new Vector3(0, 360 / statesOfRotation, 0), rotationDuration, UpdateRotation));
        
    }

    /**
     * Updates the rotation value. To be called at the end of the animation.
     */ 
    private void UpdateRotation()
    {
        currentRotation++;
        if(manager != null) manager.NotifyOfPillarRotation();
        Debug.Log(currentRotation % (statesOfRotation / sidesOfShape) + " -" + isAtTargetRotation(0));

		rotating = false;
    }
}
