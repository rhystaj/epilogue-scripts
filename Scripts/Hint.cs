using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : InfoText {

    public string hintText;
    public Text hintTextDisplay;
    public string hintKey;
    public float duration;

    private bool mousedOver;
    private bool showingHint;

    private void Update()
    {
        if (!mousedOver || showingHint) return;

        if (Input.GetKeyDown(hintKey) && hintTextDisplay.text.Equals(""))
        {
            showingHint = true;
            StartCoroutine(ShowHint());
        }
    }

    protected override void ShowText()
    {
        mousedOver = true;
        base.ShowText();
    }

    protected override void HideText()
    {
        mousedOver = false;
        base.HideText();
    }

    private IEnumerator ShowHint()
    {
        hintTextDisplay.text = hintText;
        yield return new WaitForSeconds(duration);
        hintTextDisplay.text = "";

        showingHint = false;
    }
}
