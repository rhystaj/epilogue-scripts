using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationUnit : MonoBehaviour {

    public CombinationPuzzleManager manager;
	public int sides;
    public int targetValue;
    public float rotationDuration;

    private int currentValue = 1;

	private bool rotating;

    // Use this for initialization
    void Start()
    {
        manager.Subscribe(this);
    }

    public void OnMouseDown()
    {
        //Rotate the cube and call the cube turned event.
        
		rotating = true;
		GetComponent<AudioSource> ().Play ();

        StartCoroutine(OverTime.Rotate(transform, new Vector3(-90, 0, 0), rotationDuration, AfterRotate));
    }

    public bool IsAtTargetValue()
    {
        return currentValue == targetValue;
    }

    private void AfterRotate()
    {
        currentValue = currentValue % sides + 1; //Cycle the value.
        Debug.Log(currentValue);

		rotating = false;
        manager.Notify();
    }
}
