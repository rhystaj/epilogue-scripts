using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOutcome : Outcome
{
    public override void Activate()
    {
        Debug.Log("Solved");
    }
		
}
