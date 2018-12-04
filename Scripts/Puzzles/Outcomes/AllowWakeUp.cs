using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AllowWakeUp : Outcome
{
    public Text message;
    public string wakeUpKey;
    public IslandProgressionManager manager;

    bool activated;

    // Use this for initialization
    void Start()
    {
        message.gameObject.SetActive(false);
    }

    public override void Activate()
    {
        message.gameObject.SetActive(true);
        activated = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(wakeUpKey) && activated)
            manager.TransitionScenes(true);
    }
}
