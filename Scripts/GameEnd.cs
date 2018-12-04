using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {

    public AudioSource music;
    public AudioSource ambience;
    public Image fade;

    private void OnMouseDown()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        StartCoroutine(OverTime.ChangeOpacity(fade, 1f, 255f, 0.1f, null));
        GetComponent<AudioSource>().Play();
        music.Pause();
        ambience.Pause();

        yield return new WaitForSeconds(3f);

        Application.Quit();
    }

}
