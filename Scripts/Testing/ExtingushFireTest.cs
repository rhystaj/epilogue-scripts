using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtingushFireTest : MonoBehaviour {

    public ExtinguishableFire fire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            fire.Extinguish();
            Debug.Log("f");
        }
    }
}
