using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDeleteTest : MonoBehaviour {

    public FadeOutDelete obj;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f"))
        {
            obj.Delete();
            Debug.Log("f");
        }
	}
}
