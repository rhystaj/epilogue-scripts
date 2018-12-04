using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableSteak : MonoBehaviour {

    public int correctSide;
    public int correctAngleState;
    public float angleWhenCorrect;
    public float angleWhenIncorrect;
    public Backboard backboard;

    private int side = 1;
    private int angleState = 0;
    private int lastNonZeroAngleState = 1;

    private void OnMouseDown()
    {
        if(backboard.GetState() == Backboard.STEAKS_PLACED)
        {
			GetComponent<AudioSource> ().Play ();

            transform.Rotate(0, 180, 0);
            side = side % 2 + 1; //Toggle rotation value between 1 and 2.

            //Reset angle to 0;
            angleState = 0;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            lastNonZeroAngleState *= -1;
        }
        else if(backboard.GetState() == Backboard.CAPS_PLACED)
        {
			GetComponent<AudioSource> ().Play ();

            if (angleState == 0 && (lastNonZeroAngleState == 1)) ChangeAngle(-1);
            else if (angleState == 1) ChangeAngle(-1);
            else ChangeAngle(1);
        }
    }

    private void ChangeAngle(int multiplier)
    {
        if ((angleState == correctAngleState && side == correctSide) || (side == correctSide && angleState + multiplier == correctAngleState)) {
            transform.Rotate(0, 0, angleWhenCorrect * multiplier);
        }
        else {
            transform.Rotate(0, 0, angleWhenIncorrect * multiplier);
        }

        angleState += multiplier;

        if (angleState != 0) lastNonZeroAngleState = angleState;
    }

}
