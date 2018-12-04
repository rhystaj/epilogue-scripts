using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarControlButton : MonoBehaviour {

    public MovingPillarPuzzleManager manager;

    public MovingPillar moveLeft;
    public MovingPillar moveRight;
    public MovingPillar moveUp;
    public MovingPillar moveDown;


    public float moveDuration;

	private PressableButton press;

	private bool locked;

    private void Start()
    {
        manager.Subscribe(this);
		press = GetComponent<PressableButton> ();
    }

    private void OnMouseDown()
    {
		StartCoroutine (press.PressAndRelease (AfterPress));
    }
    
	private void AfterPress(){
		if (!locked) StartCoroutine(MovePillars());
	}

    /**
     * Move the pillars in thier respective directions, if they can be moved, and lock use of button until move is over.
     */ 
    private IEnumerator MovePillars()
    {

        locked = true;
        bool movePossible = false;

        //Try to move each of the pillars in their respective directions and if they can, set move possible to true.
        if (moveLeft != null && moveLeft.Move(MovingPillar.MoveDirection.Left, moveDuration, OnMoveFinish)) movePossible = true;
        if (moveRight != null && moveRight.Move(MovingPillar.MoveDirection.Right, moveDuration, OnMoveFinish)) movePossible = true;
        if (moveUp != null && moveUp.Move(MovingPillar.MoveDirection.Up, moveDuration, OnMoveFinish)) movePossible = true;
        if (moveDown != null && moveDown.Move(MovingPillar.MoveDirection.Down, moveDuration, OnMoveFinish)) movePossible = true;

        //If a move is possible, lock the use of the button until it is over.
        if (movePossible) yield return new WaitForSeconds(moveDuration);

        locked = false;
        yield return null;

    }

    private void OnMoveFinish()
    {
        manager.Notify();
    }

}
