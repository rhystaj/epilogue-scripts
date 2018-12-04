using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGroup : MonoBehaviour {

	public float angleFromCentre;
	public AudioSource movementSoundSource;

    private RotatingPillar groupCentre; //The pillar the other pillars are based around.

    //The positions of the outer pillars when they are next to the centre.
    private Dictionary<GroupRotatingPillar, Vector3> outerPillarsCorrectPosition = new Dictionary<GroupRotatingPillar, Vector3>(); 

    /**
     * Identify where the children of the group should be in relation to the centre based on the radius and put them there.
     */ 
    public void InitialiseAndPositionGroup(float radius)
    {

        Debug.Log("Group Children: " + transform.childCount);

        groupCentre = GetComponent<RotatingPillar>();

        for(int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {

            //Get the RotatingPillar component from the child, skip it if it doesn't have one.
            GroupRotatingPillar pillar = transform.GetChild(childIndex).GetComponent<GroupRotatingPillar>();
            if (pillar == null) continue;

            outerPillarsCorrectPosition.Add(pillar, pillar.transform.localPosition);

            pillar.transform.localPosition = GetChildPosition(pillar, radius);
        }
    }

    public bool CentrePillarAtCorrectRotation()
    {
        return groupCentre.isAtTargetRotation(0);
    }

    /**
     * Returns whether all the pillars in the group are at the correct rotation.
     */ 
    public bool OuterPillarsAtCorrectRotation()
    {

        foreach(RotatingPillar pillar in outerPillarsCorrectPosition.Keys)
        {
            //As outer pillars can be relatively correct, based on the rotation of the centre pillar, treat the possible centre pillar rotation as different stages.
            if (!pillar.isAtTargetRotation(0)) return false;
        }

        return true;

    }

	/**
	 * Plays the movement sound of this pillar group and its childen.
	 */ 
	public void PlayMovementSounds(){
		movementSoundSource.Play ();
		foreach (GroupRotatingPillar pillar in outerPillarsCorrectPosition.Keys)
			pillar.movementSoundSource.Play ();

	}

    /**
     * Brings the outer pillars to the middle of the group over the given time.
     */ 
    public IEnumerator BringPillarsToCentre(float duration, float stopAtPercentage)
    {

        foreach(GroupRotatingPillar pillar in outerPillarsCorrectPosition.Keys)
        {
            pillar.enabled = false;
            
			pillar.movementSoundSource.Play ();
			StartCoroutine(OverTime.Move(pillar.transform, pillar.transform.localPosition, outerPillarsCorrectPosition[pillar], duration, stopAtPercentage, null));
        }

        yield return new WaitForSeconds(duration);

    }

    /**
     * Position the pillars at the distance aroud the centre over the given time.
     */ 
    public IEnumerator SeparatePillars(float duration, float endRadius)
    {

        foreach (GroupRotatingPillar pillar in outerPillarsCorrectPosition.Keys)
        {
			pillar.movementSoundSource.Play ();
			StartCoroutine(OverTime.Move(pillar.transform, pillar.transform.localPosition, GetChildPosition(pillar, endRadius), duration, null));
        }

        yield return new WaitForSeconds(duration);

        foreach (RotatingPillar pillar in outerPillarsCorrectPosition.Keys) pillar.enabled = true;

    }

    private Vector3 GetChildPosition(GroupRotatingPillar child, float radius)
    {

        return new Vector3(
           radius * Mathf.Cos(child.angleFromGroupCentre * Mathf.Deg2Rad),
           0f,
           radius * Mathf.Sin(child.angleFromGroupCentre * Mathf.Deg2Rad)
       );

    }

}
