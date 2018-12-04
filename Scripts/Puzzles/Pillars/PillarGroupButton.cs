using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGroupButton : MonoBehaviour {

    public GroupedPillarPuzzleManager manager;

    private PressableButton press;

    private void Start()
    {
        press = GetComponent<PressableButton>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Interact!");

        StartCoroutine(press.PressAndRelease(AfterPress));
    }

    private void AfterPress()
    {
        manager.Notify();
    }

}
