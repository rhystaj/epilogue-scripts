using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A refernce pillar for the puzzle solution.
 */ 
public class ComparisionPillar : MonoBehaviour {

    public MultiStagePillarPuzzleManager manager;
    public int[] stages;

    private void Start()
    {
        manager.Subscribe(this);
    }

}
