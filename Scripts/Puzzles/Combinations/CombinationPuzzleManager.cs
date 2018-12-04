using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPuzzleManager : MonoBehaviour {

    public Outcome outcome;

    private HashSet<CombinationUnit> units = new HashSet<CombinationUnit>();
    private bool solved;

    public void Subscribe(CombinationUnit unit)
    {
        units.Add(unit);
    }

    public void Notify()
    {

        //If the puzzle has already been solved, don't continue;
        if (solved) return;

        Debug.Log(units.Count);

        //If combination is incorrect, don't proceed.
        foreach (CombinationUnit unit in units)
            if (!unit.IsAtTargetValue()) return;

        //Else, raise the bridge and set as solved.
        Debug.Log("Correct!");
        outcome.Activate();
        solved = true;

    }
}
