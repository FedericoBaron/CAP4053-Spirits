using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {   
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("IntroSequence");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        Time.timeScale = 1f;
    }

    public void ControlsScene()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Controls");
    }
}
