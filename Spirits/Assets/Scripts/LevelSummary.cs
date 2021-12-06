using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSummary : MonoBehaviour
{
    public Text Money;
    public Text Spirits;
    public Text Recipes;
    public Text TotalMoney;

    public void Update(){
        GameObject player = GameObject.Find("Bartender");
        int ghostsCaptured = -1;
        
        if (player != null){
            ghostsCaptured = player.GetComponent<Player_Combat>().ghostsCaptured;
            // Destroy(player);
            int moneyMade = ghostsCaptured * 30;
            int totalMoney = player.GetComponent<Player_Combat>().totalMoney + moneyMade;
            
            // set total money
            if (TotalMoney != null && Money != null && Recipes != null && Spirits != null){
                Debug.Log(totalMoney);
                Debug.Log(TotalMoney);
                Money.text = "Money: " + moneyMade.ToString();
                TotalMoney.text = "Total Money: " + totalMoney.ToString();
                Spirits.text = "Spirits Captured: " + ghostsCaptured.ToString();
            }
            player.GetComponent<Player_Combat>().ghostsCaptured = 0;
            player.GetComponent<Player_Combat>().totalMoney = totalMoney;
        }
    }

    public void RestartButton(){
        SceneManager.LoadScene("Tutorial");
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
    }

    public void NextButton(){
        GameObject player = GameObject.Find("Bartender");
        player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        SceneManager.LoadScene("Forest");
        GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Pause();
    }

    public void MapButton(){
        // GameObject player = GameObject.Find("Bartender");
        // player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Map");
    }

    public void MainMenuButton(){
        // GameObject player = GameObject.Find("Bartender");
        // player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("TitleScreen");
    }
}
