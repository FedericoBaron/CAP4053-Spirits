using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public void OpenTutorial()
    {
        Player_Combat player;
        player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        if (player != null){
            Player_Combat.recipesMade = 0;
            player.ghostsCaptured = 0;
        }
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Tutorial");
        GetComponent<AudioSource>().Play();

    }

    public void OpenForest()
    {
        Player_Combat player;
        player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        if (player != null){
            Player_Combat.recipesMade = 0;
            player.ghostsCaptured = 0;
        }
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Forest");
        GetComponent<AudioSource>().Play();
    }

    public void OpenApartment()
    {
         Player_Combat player;
        player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        if (player != null){
            Player_Combat.recipesMade = 0;
            player.ghostsCaptured = 0;
        }
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("ApartmentWBarriers");
        GetComponent<AudioSource>().Play();
    }

    public void OpenMansion()
    {
        Player_Combat player;
        player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        if (player != null){
            Player_Combat.recipesMade = 0;
            player.ghostsCaptured = 0;
        }
        Time.timeScale = 1f;
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Mansion");
        GetComponent<AudioSource>().Play();
    }
}

