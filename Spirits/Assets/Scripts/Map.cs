using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void OpenForest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Forest");
    }

    public void OpenApartment()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ApartmentWBarriers");
    }

    public void OpenMansion()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mansion");
    }
}

