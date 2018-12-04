using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPillarPuzzleResetButton : MonoBehaviour {

    public MovingPillarPuzzleManager manager;

    private PressableButton press;

    private void Start()
    {
        press = GetComponent<PressableButton>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(press.PressAndRelease(AfterPress));
    }

    private void AfterPress()
    {
        manager.ResetPuzzle();
    }

}
