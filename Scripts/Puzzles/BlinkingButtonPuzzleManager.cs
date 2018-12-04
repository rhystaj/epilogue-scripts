using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingButtonPuzzleManager : ButtonOrderManager {

    public int[] physicalPositionsByOrder;
    public float blinkDuration;

    public void InitialButtonPushed(BlinkPuzzleInitialButton initalButton)
    {
        base.ResetPuzzle();

        StartCoroutine(BlinkButton(initalButton.GetComponent<IlluminatingObject>(), physicalPositionsByOrder[1]));
    }

    protected override void SpecialAction(OrderButton button, int buttonsPushed, bool inOrderSoFar)
    {

        if (inOrderSoFar && buttonsPushed + 1 < physicalPositionsByOrder.Length)
            StartCoroutine(BlinkButton(button.GetComponent<IlluminatingObject>(), physicalPositionsByOrder[buttonsPushed + 1]));

    }

    private IEnumerator BlinkButton(IlluminatingObject illuminator, int blinks)
    {

        for(int i = 0; i < blinks; i++)
        {
            StartCoroutine(illuminator.Illuminate(blinkDuration / 2));
            yield return new WaitForSeconds(blinkDuration / 2);
            StartCoroutine(illuminator.Deilluminate(blinkDuration / 2));
            yield return new WaitForSeconds(blinkDuration / 2);
        }

    }

}
