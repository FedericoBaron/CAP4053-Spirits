using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(7);
    }

    public void OpenForest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void OpenApartment()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void OpenMansion()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }
}

