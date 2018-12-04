using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillar : MonoBehaviour {

    public Vector2 offset; //The x and y offsets the pillar will move from the centre.
    public int startingXPositionNumber; //The relative number of the starting x pos, based on the offset (must be from -1 to 1).
    public int startingYPositionNumber; //The relative number of the starting y pos, based on the offset (must be from -1 to 1).

    private Vector2 positionNumbers; //The relative postion of the pillar, based on the offset.

    private bool xLocked;
    private bool yLocked;

    public enum MoveDirection
    {
        Up, Down, Left, Right
    }

    private void Start()
    {
        //Intialise the postion based on the given values.
        positionNumbers = new Vector2(startingXPositionNumber, startingYPositionNumber);
        transform.Translate(offset.x * startingXPositionNumber, offset.y * startingYPositionNumber, 0);

    }

    /**
     * Get the number representing the postion of the pillar.
     */
    public Vector2 GetPositionNumbers()
    {
        return this.positionNumbers;
    }

    /**
     * Move the pillar by the appropriate offset value based on the given direction over the given time, if it can be moved.
     * Also returns whether it can be moved.
     */ 
    public bool Move(MoveDirection direction, float duration, Action onMoveFinish)
    {
        Vector3 startPostion = transform.localPosition;
        Vector3 distance; //TBD

        //Determine the distance to move based on the direction.
        if (direction == MoveDirection.Up && positionNumbers.y <= 0)
        {
            distance = new Vector3(0, offset.y, 0);
            positionNumbers.y += 1;
        }
        else if (direction == MoveDirection.Down && positionNumbers.y >= 0)
        {
            distance = new Vector3(0, -1 * offset.y, 0);
            positionNumbers.y -= 1;
        }
        else if (direction == MoveDirection.Left && positionNumbers.x >= 0)
        {
            distance = new Vector3(-1 * offset.x, 0, 0);
            positionNumbers.x -= 1;
        }
        else if (direction == MoveDirection.Right && positionNumbers.x <= 0)
        {
            distance = new Vector3(offset.x, 0, 0);
            positionNumbers.x += 1;
        }
        else return false;
        
        Debug.Log(distance);

        //Animate translation.
		GetComponent<AudioSource>().Play();
		StartCoroutine(OverTime.Translate(transform, distance, duration, onMoveFinish));
        return true;
    }

    /**
     * Return the pillars to thier starting position.
     */ 
    public void ResetPosition()
    {

        float xDif = startingXPositionNumber - positionNumbers.x;
        float yDif = startingYPositionNumber - positionNumbers.y;

        positionNumbers = new Vector2(startingXPositionNumber, startingYPositionNumber);

        transform.Translate(new Vector3(offset.x * xDif, offset.y * yDif, 0));

    }

    /**
     * Checks wheter the pillars have been centred, and also
     */ 
    public bool PillarCentred()
    {
        return positionNumbers.x == 0 && positionNumbers.y == 0;
    }
	
}
