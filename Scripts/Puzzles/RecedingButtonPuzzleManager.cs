using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecedingButtonPuzzleManager : ButtonOrderManager {

    private List<OrderButton> buttonsInOrder = new List<OrderButton>();

    public override void AddButton(OrderButton button)
    {
        base.AddButton(button);

        while (buttonsInOrder.Count <= button.numberInOrder)
            buttonsInOrder.Add(null);

        buttonsInOrder[button.numberInOrder] = button;
    }

    protected override void SpecialAction(OrderButton button, int buttonsPushed, bool inOrderSoFar)
    {
        if (button.numberInOrder > 1)
            buttonsInOrder[button.numberInOrder - 1].Press(false);
    }

}
