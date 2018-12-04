using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomProgressionManager : ProgressionManager
{
    
    public BedroomState[] states; //The states of the bedroom stored by thier state values.

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		base.OnSceneLoaded (scene, mode);
		ConfigureBedroom();

		StartCoroutine(OverTime.ChangeOpacity (fadeImage, 255f, 0f, fadeDuration, null));
    }

    /**
     * Shows and hides the appropriate elements at the given stages.
     */ 
    private void ConfigureBedroom()
    {
        foreach(GameObject obj in states[State].show)
            obj.SetActive(true);

        foreach (GameObject obj in states[State].hide)
            obj.SetActive(false);
    }

    /**
     * A configuration of the bedroom.
     */
    [System.Serializable]
    public class BedroomState
    {
        public GameObject[] show;
        public GameObject[] hide;
    }

}
