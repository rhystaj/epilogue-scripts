using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrderManager : AestheticSave {


	public Outcome result; //What happens when the puzzle is solved.

	HashSet<OrderButton> buttons = new HashSet<OrderButton>();

	private int buttonsPushed = 0;
	private bool inOrderSoFar = true;
	private bool puzzleSolved = false;

	/**
     * Adds a child button to the set of buttons.
     */ 
	public virtual void AddButton(OrderButton button)
	{
		this.buttons.Add(button);
	}

	/**
     * React to the press of a button, if it is one of its children.
     * Checks as to wheter an entire sequence of buttons have been pressed, and whether they have been pressed in the right order and react accordingly.
     */ 
	public virtual void Notify(OrderButton button, bool perfomSpecialAction)
	{

		if (puzzleSolved) return; //Don't do anthing if the puzzle has already been solved.
		if (!buttons.Contains(button)) return; //Only respond to the push if the button has been subcribed to the parent.

		buttonsPushed++;
		if (button.numberInOrder != buttonsPushed) inOrderSoFar = false;

		if(perfomSpecialAction) SpecialAction(button, buttonsPushed, inOrderSoFar);

		if (buttonsPushed != buttons.Count) return;

		if (inOrderSoFar)
		{
			SetSolved();
		}
		else
		{
			ResetPuzzle();
		}
	}

	protected virtual void SetSolved()
	{
		puzzleSolved = true;
		result.Activate();
	}

	/**
     *  //Revert manager to initial state.
	*/
	public virtual void ResetPuzzle()
	{
		foreach (OrderButton b in buttons) b.ResetButton();
		buttonsPushed = 0;
		inOrderSoFar = true;
	}

	/**
     * A method with the sole purpose of being overridden for subtyps of the class to inset thier own behavior.
     */ 
	protected virtual void SpecialAction(OrderButton button, int buttonsPushed, bool inOrderSoFar) { }

	public override void RestoreAppearance()
	{
		foreach (OrderButton button in buttons)
			button.Lock();
	}
}
