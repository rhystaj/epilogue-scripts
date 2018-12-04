using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest : MonoBehaviour {

	public ProgressionManager manager;
    public bool  disabled;

	private void OnMouseDown(){

        if (disabled) return;

        manager.TransitionScenes (false);
	}
}
