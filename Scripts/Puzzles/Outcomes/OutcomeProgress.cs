using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutcomeProgress : MonoBehaviour {

    public int puzzlesToSolve;
    public Outcome outcome;
	public AudioSource musicPlayer;
	public float fadeDuration;

    private int puzzlesSolved = 0;
    private HashSet<Outcome> ActivatedOutcomes = new HashSet<Outcome>();

    public void UpdateProgress(Outcome activator)
    {
        if (ActivatedOutcomes.Contains(activator)) return;

        puzzlesSolved++;

        if(puzzlesSolved >= puzzlesToSolve)
        {
			StartCoroutine(OverTime.FadeVolume (musicPlayer, fadeDuration));
            outcome.Activate();
        }

    }

}
