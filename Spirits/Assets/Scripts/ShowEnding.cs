using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowEnding : MonoBehaviour
{
    public void OnEnable()
    {
        SceneManager.LoadScene("GameWon", LoadSceneMode.Single);
    }
}