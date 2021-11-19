using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenForest()
    {
        SceneManager.LoadScene(3);
    }

    public void OpenApartment()
    {
        SceneManager.LoadScene(4);
    }

    public void OpenMansion()
    {
        SceneManager.LoadScene(5);
    }
}

