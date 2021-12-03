using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenTutorial()
    {
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Tutorial");
        GetComponent<AudioSource>().Play();
    }

    public void OpenForest()
    {
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Forest");
        GetComponent<AudioSource>().Play();
    }

    public void OpenApartment()
    {
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("ApartmentWBarriers");
        GetComponent<AudioSource>().Play();
    }

    public void OpenMansion()
    {
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Mansion");
        GetComponent<AudioSource>().Play();
    }
}

