using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenTutorial()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Tutorial");
    }

    public void OpenForest()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Forest");
    }

    public void OpenApartment()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("ApartmentWBarriers");
    }

    public void OpenMansion()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Mansion");
    }
}

