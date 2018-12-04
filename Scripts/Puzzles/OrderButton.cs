using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A button in a series to be pressed in order.
 */ 
public class OrderButton : MonoBehaviour {

    public int numberInOrder;
    public ButtonOrderManager manager;
    public bool changeIlluminationWithPress;

    private bool locked;

    private PressableButton press; //Used to animate the button press.
    private IlluminatingObject illuminator;

    private void Start()
    {
        if(manager != null) manager.AddButton(this);
        press = GetComponent<PressableButton>();
        illuminator = GetComponent<IlluminatingObject>();

        if (illuminator != null && changeIlluminationWithPress) illuminator.Illuminate();
    }

    private void OnMouseDown()
    {
        Press(true);
    }

    public void Press(bool haveSpecialActionPerformed)
    {
        if (locked) return;

        Action afterPress;
        if (haveSpecialActionPerformed) afterPress = NoitificationWithSpecialAction;
        else afterPress = NotificationWithoutSpecialAction;

        StartCoroutine(press.PressAndLock(afterPress));
        locked = true;
    }

    public void ResetButton()
    {
        if (locked)
        {
            StartCoroutine(press.UnlockAndRelease(Reilluminate));
            locked = false;
        }
    }
 
    protected virtual void NoitificationWithSpecialAction()
    {
        manager.Notify(this, true);

        if (illuminator != null && changeIlluminationWithPress) illuminator.Deilluminate();
    }

    private void NotificationWithoutSpecialAction()
    {
        manager.Notify(this, false);

        if (illuminator != null && changeIlluminationWithPress) illuminator.Deilluminate();
    }

    private void Reilluminate()
    {
        if (illuminator != null && changeIlluminationWithPress) illuminator.Illuminate();
    }

    public void Lock()
    {
        if (locked) return;

        locked = true;
        press.Lock();
        if (illuminator != null && changeIlluminationWithPress) illuminator.Deilluminate();
    }

    public void Unlock()
    {
        if (!locked) return;

        locked = false;
        press.Unlock();
        Reilluminate();
    }
}
