using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class ProgressionManager : MonoBehaviour
{

    public const string SAVE_KEY = "savePref";

    public string transitionScene; //The scene that will be transitioned to.
	public Transform player; //The player that is being loaded into the scene.
    public Text toDisable;
    public AudioSource musicPlayer; //The source used to play the level music.
	public AudioSource ambientSoundPlayer; //The source used to play the ambient sound.
	public AudioSource sceneTransitionPlayer;
	public Image fadeImage;
	public float fadeDuration;
	public float musicStartDelay;
	public LoadState[] loadStates; //The state of the scene when it is first loaded.


    private static int state = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /**
     * Subsribed to the sceneLoaded delegate and called when scene is loaded.
     */
	protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if(state > 0)player.localPosition = loadStates [state].startingPosition;

		if (ambientSoundPlayer != null) {
			ambientSoundPlayer.clip = loadStates [state].levelAmbience;
			if (ambientSoundPlayer.clip != null)
				ambientSoundPlayer.Play ();
		}

		if (musicPlayer != null && musicPlayer.clip != null)
			musicPlayer.PlayDelayed (musicStartDelay);

        if(loadStates[state].toDisable != null)
        {
            loadStates[state].toDisable.disabled = true;
        }

        if (loadStates[state].promptToDisable != null)
            loadStates[state].promptToDisable.text = "";

	}

    /**
     * Gets the current state of ALL managers.
     */ 
    protected int State
    {
        get { return state; }
        set { state = value; }
    }
    

    /**
     * Load the set transition scene, and unload the current one.
     */ 
    public virtual void TransitionScenes(bool save)
    {
        if(fadeImage != null)
		    StartCoroutine (OverTime.ChangeOpacity(fadeImage, 1f, 255f, 0.1f, null));

        if(toDisable != null)
            toDisable.gameObject.SetActive(false);

		StartCoroutine (PlayClipAndWait (LoadNextScene, save));
        
    }

	private IEnumerator PlayClipAndWait(Action after, bool save){

		if (loadStates [state].sceneTransition != null) {

			sceneTransitionPlayer.clip = loadStates [state].sceneTransition;
            ambientSoundPlayer.Stop();
            sceneTransitionPlayer.Play ();

            if (save) Save();

			Debug.Log (sceneTransitionPlayer.clip.length);
			yield return new WaitForSeconds (sceneTransitionPlayer.clip.length);

		}

		if (after != null)
			after ();

	}

	private void LoadNextScene(){

		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(transitionScene);
		SceneManager.UnloadSceneAsync(currentScene.name);

	}

    /**
     * Increment the state and save the value.
     */
    public void Save()
    {
        state++;
        Debug.Log("State: " + state);
        PlayerPrefs.SetInt(SAVE_KEY, state);
        PlayerPrefs.Save();
    }

    /**
     * Returns whether a save exists.
     */ 
    public bool SaveExists()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

	/**
	 * A tuple for loading into the scene.
	 */
	[System.Serializable]
	public class LoadState{
		public AudioClip levelAmbience;
		public Vector3 startingPosition;
		public AudioClip sceneTransition;
        public TransitionTest toDisable;
        public InfoText promptToDisable;
	}
}

