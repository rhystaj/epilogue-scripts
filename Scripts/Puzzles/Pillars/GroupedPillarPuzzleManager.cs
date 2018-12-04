using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupedPillarPuzzleManager : PillarPuzzleManager {

    public float groupsDistanceFromCentre;
    public float outerPillarsDistanceFromCentre;
    public float incorrectStopAtRatio;
    public float duration;
   
    private Dictionary<PillarGroup, Vector3> groupsCorrectPositions = new Dictionary<PillarGroup, Vector3>();

    private void Start()
    {
        Debug.Log("Setting Up Puzzle!");
        Debug.Log(transform.childCount);
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {

            //Get the RotatingPillar component from the child, skip it if it doesn't have one.
            PillarGroup group = transform.GetChild(childIndex).GetComponent<PillarGroup>();
            if (group == null) continue;

            group.InitialiseAndPositionGroup(outerPillarsDistanceFromCentre);
            groupsCorrectPositions.Add(group, group.transform.localPosition);

            group.transform.localPosition = GetChildPosition(group, groupsDistanceFromCentre);
        }
    }

    public void Notify()
    {
        StartCoroutine(CheckSolution());
    }

    private IEnumerator CheckSolution()
    {

        int correctGroups = 0;

        //Check the groups and then animate them going into the centre.
		foreach(PillarGroup group in groupsCorrectPositions.Keys)
        {

            if (group.OuterPillarsAtCorrectRotation())
            {
                Debug.Log("Correct Outer");

                //If the outer pillars are correctly rotated, bring them all the way to the centre and add 1 to the correct entried.
                correctGroups++;
                StartCoroutine(group.BringPillarsToCentre(duration, 1));
            }
            else StartCoroutine(group.BringPillarsToCentre(duration, incorrectStopAtRatio));

        }

        yield return new WaitForSeconds(duration);

		if (correctGroups >= groupsCorrectPositions.Count)
        {
            //The groups have been assembled correctly based on the middle pillar, now check whole group arrangment.
			foreach (PillarGroup group in groupsCorrectPositions.Keys)
            {
				if (group.CentrePillarAtCorrectRotation ()) {
					Debug.Log ("Correct Inner");

					correctGroups--;

					group.PlayMovementSounds ();
					StartCoroutine (OverTime.Move (group.transform, group.transform.localPosition, groupsCorrectPositions [group], duration, 1, null));
				} else {
					group.PlayMovementSounds ();
					StartCoroutine (OverTime.Move (group.transform, group.transform.localPosition, groupsCorrectPositions [group], duration, incorrectStopAtRatio, null));
				}
            }

            yield return new WaitForSeconds(duration);

            if (correctGroups <= 0) outcome.Activate();
            else
            {
				foreach (PillarGroup group in groupsCorrectPositions.Keys) {
					group.PlayMovementSounds ();
					StartCoroutine (OverTime.Move (group.transform, group.transform.localPosition, GetChildPosition (group, groupsDistanceFromCentre), duration, null));
				}

                yield return new WaitForSeconds(duration);

				foreach (PillarGroup group in groupsCorrectPositions.Keys) {
					group.PlayMovementSounds ();
					StartCoroutine (group.SeparatePillars (duration, outerPillarsDistanceFromCentre));
				}
            }

        }
        else
        {
            //There is a group assembled incorrectly, don't proceed to next stage.
			foreach (PillarGroup group in groupsCorrectPositions.Keys)
                StartCoroutine(group.SeparatePillars(duration, outerPillarsDistanceFromCentre));
        }

    }

    private Vector3 GetChildPosition(PillarGroup child, float radius)
    {

        return new Vector3(
			radius * Mathf.Cos(child.angleFromCentre * Mathf.Deg2Rad),
            0f,
			radius * Mathf.Sin(child.angleFromCentre * Mathf.Deg2Rad)
        );

    }

    public static float CalculateAngleBetweenPillars(Vector3 centrePillarPosition, Vector3 outerPillarPosition)
    {
        
        Vector2 centrePos2D = new Vector2(centrePillarPosition.x, centrePillarPosition.z);
        Vector2 pillarPos2D = new Vector2(outerPillarPosition.x, outerPillarPosition.z);

        float angle = 180 + Vector2.Angle(Vector2.right, centrePos2D - pillarPos2D);

        Debug.Log("Angle: " + angle);
        return angle;

    }

}
