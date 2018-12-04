using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandProgressionManager : ProgressionManager
{
    public IslandState[] states;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		base.OnSceneLoaded(scene, mode);
		ConfigureIsland();

		StartCoroutine(OverTime.ChangeOpacity (fadeImage, 255f, 0f, fadeDuration, null));
    }

    public void TransitionScenes(int state)
    {
        State = state;
        base.TransitionScenes(false);
    }

    public override void TransitionScenes(bool save)
    {
        base.TransitionScenes(save);
    }

    private void ConfigureIsland()
    {
        foreach (AestheticSave saveable in states[State].savables)
            saveable.RestoreAppearance();
    }

    /**
     * Stores a configuration of the island.
     */ 
    [System.Serializable]
	public class IslandState
    {
        public AestheticSave[] savables; //The objects to have thier visual state changed for consistency.
    }

}
