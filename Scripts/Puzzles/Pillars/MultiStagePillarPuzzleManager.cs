using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStagePillarPuzzleManager : PillarPuzzleManager {

    public int stages;

    private int stage = 0;
    
    private HashSet<ComparisionPillar> referencePillars = new HashSet<ComparisionPillar>();

    private bool solved;

    public void Subscribe(ComparisionPillar pillar)
    {
        referencePillars.Add(pillar);
    }

    public void NotifyOfPillarRotation()
    {

        if (!AllPillarsInCorrectRotation(stage)) return;

        if (stage < stages - 1)
        {
            TransitionStage();
        }
        else
        {
            outcome.Activate();
        }

    }

    private void TransitionStage()
    {

        //NOTE: Need to find a way to disable the roatation of all pillars during transition.

        stage++;

        float maxDuration = 0;

        //Hide and show the appropriate rotating pillars.
        foreach (RotatingPillar pillar in GetPillars())
        {
            PillarShowHide showHide = pillar.GetComponent<PillarShowHide>();

            if (pillar.targetRelativeRotationsAtStages[stage] == -1) showHide.Hide();
            else showHide.Show();

            pillar.enabled = false; //Lock the pillars so they can't be used over the animation.
            if (showHide.duration > maxDuration) maxDuration = showHide.duration; //Find the max duration of the show/hide to determine how long to disable the pillars.
        }

        //Hide and show the appropriate reference pillars.
        foreach (ComparisionPillar pillar in referencePillars)
        {
            PillarShowHide showHide = pillar.GetComponent<PillarShowHide>();

            if (new HashSet<int>(pillar.stages).Contains(stage + 1)) showHide.Show();
            else showHide.Hide();

            if (showHide.duration > maxDuration) maxDuration = showHide.duration;
        }

        StartCoroutine(WaitAndReenablePillars(maxDuration));

    }

    /**
     * Renable the pillars after the given time.
     */ 
    private IEnumerator WaitAndReenablePillars(float waitDuration)
    {

        Debug.Log("Wait duration");

        yield return new WaitForSeconds(waitDuration);
        foreach (RotatingPillar pillar in GetPillars())
            pillar.enabled = true;

        yield return null;

    }

}
