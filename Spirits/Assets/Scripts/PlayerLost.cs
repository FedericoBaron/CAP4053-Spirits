using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerLost : MonoBehaviour
{
    public Text moneyText;

    public void Start(){
        GameObject player = GameObject.Find("Bartender");
        int score = -1;
        if (player != null){
            score = player.GetComponent<Player_Combat>().totalMoney;
            Destroy(player);
        }
        moneyText.text = "Money: " + score.ToString();
    }

    public void RestartButton(){
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Tutorial");
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
        
    }

    public void MainMenuButton(){
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("TitleScreen");
    }
}
