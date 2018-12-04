using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillarTesting : MonoBehaviour {

    public MovingPillar testPillar;

    private void Update()
    {
        if (Input.GetKeyDown("h")) testPillar.Move(MovingPillar.MoveDirection.Down,  1, null);
        if (Input.GetKeyDown("y")) testPillar.Move(MovingPillar.MoveDirection.Up,    1, null);
        if (Input.GetKeyDown("g")) testPillar.Move(MovingPillar.MoveDirection.Left,  1, null);
        if (Input.GetKeyDown("j")) testPillar.Move(MovingPillar.MoveDirection.Right, 1, null);

    }

}
