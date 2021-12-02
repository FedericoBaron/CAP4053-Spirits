using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (gameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }   
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false; 
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true; 
    }

    public void LoadMap(){
        Debug.Log("Map Loaded");
        SceneManager.LoadScene("Map");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
    }

    public void LoadMenu(){
        Debug.Log("Menu Loaded");
        SceneManager.LoadScene("TitleScreen");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
    }

    public void Quit(){
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
