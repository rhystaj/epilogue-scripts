using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallTest : MonoBehaviour {

    public RewardRock rock;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) rock.Activate();
	}
}
