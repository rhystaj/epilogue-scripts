using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextDisplay : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
    public void Set(string infoText)
    {
        text.text = infoText;
    }

    /**
     * Remove text from the screen, if the text being displayed is given.
     */ 
    public void Clear(string textToClear)
    {
        if (text.text.Equals(textToClear)) text.text = "";
    }
	
}
