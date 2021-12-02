using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        // GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        Time.timeScale = 1f;
    }

    public void ControlsScene()
    {
        SceneManager.LoadScene("Controls");
    }
}
