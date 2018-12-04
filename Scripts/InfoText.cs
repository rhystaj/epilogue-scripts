using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour {

    public string text;
    public InfoTextDisplay display;

    private void OnMouseEnter()
    {
        ShowText();
    }

    private void OnMouseExit()
    {
        HideText();
    }

    protected virtual void ShowText()
    {
        display.Set(text);
    }

    protected virtual void HideText()
    {
        display.Clear(text);
    }
}
