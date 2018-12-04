using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleManager : MonoBehaviour {

    public Outcome outcome;

    private HashSet<RotatingPillar> rotatingPillars = new HashSet<RotatingPillar>();

    /**
     * Add a pillar to be checked as part of the solution.
     */ 
    public void Subscribe(RotatingPillar pillar)
    {
        rotatingPillars.Add(pillar);
    }

    /**
     * Returns an array of all the pillars in the manager.
     */
    protected RotatingPillar[] GetPillars()
    {
        RotatingPillar[] result = new RotatingPillar[rotatingPillars.Count];
        rotatingPillars.CopyTo(result);
        return result;
    }

    /**
     * Determines whether all the subscribed pillars are in the correct rotation.
     */ 
    protected bool AllPillarsInCorrectRotation(int stage)
    {
        foreach (RotatingPillar pillar in rotatingPillars)
            if (!pillar.isAtTargetRotation(stage)) return false;

        return true;
    }
}
