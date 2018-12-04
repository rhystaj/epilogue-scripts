using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPuzzleInitialButton : MonoBehaviour {

    public BlinkingButtonPuzzleManager blinkManager;

    private PressableButton press;

    private void Start()
    {
        press = GetComponent<PressableButton>();
    }

    private void OnMouseDown()
    {
        Debug.Log("press");

        StartCoroutine(press.PressAndRelease(AfterPress));
    }

    private void AfterPress()
    {
        blinkManager.InitialButtonPushed(this);
    }

}
