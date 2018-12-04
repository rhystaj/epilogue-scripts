using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillarPuzzleManager : MonoBehaviour {

    public Outcome outcome;

    HashSet<PillarControlButton> buttons = new HashSet<PillarControlButton>();

    private bool solved;

    public void Subscribe(PillarControlButton button)
    {
        buttons.Add(button);
    }

    public void Notify()
    {

        if (solved) return;

        foreach(PillarControlButton button in buttons)
        {
            if (button.moveUp != null && !button.moveUp.PillarCentred()) return;
            if (button.moveDown != null && !button.moveDown.PillarCentred()) return;
            if (button.moveLeft != null && !button.moveLeft.PillarCentred()) return;
            if (button.moveRight != null && !button.moveRight.PillarCentred()) return;
        }

        outcome.Activate();
        solved = true;
    }

    public void ResetPuzzle()
    {

        if (solved) return;

        foreach (PillarControlButton button in buttons)
        {
            if (button.moveUp != null) button.moveUp.ResetPosition();
            if (button.moveDown != null) button.moveDown.ResetPosition();
            if (button.moveLeft != null) button.moveLeft.ResetPosition();
            if (button.moveRight != null) button.moveRight.ResetPosition();

        }

    }

}
