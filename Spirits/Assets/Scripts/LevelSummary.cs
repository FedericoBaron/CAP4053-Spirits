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

    public void Start(){
        GameObject player = GameObject.Find("Bartender");
        int ghostsCaptured = -1;
        if (player != null){
            ghostsCaptured = player.GetComponent<Player_Combat>().ghostsCaptured;
            // Destroy(player);
        }
        int moneyMade = ghostsCaptured * 30;

        // set total money
        int totalMoney = player.GetComponent<Player_Combat>().totalMoney + moneyMade;

        Money.text = "Money: " + moneyMade.ToString();
        TotalMoney.text = "Total Money: " + totalMoney.ToString();
        Spirits.text = "Spirits Captured: " + ghostsCaptured.ToString();

        player.GetComponent<Player_Combat>().ghostsCaptured = 0;
        player.GetComponent<Player_Combat>().totalMoney = totalMoney;
    }

    public void RestartButton(){
        SceneManager.LoadScene("Tutorial");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
    }

    public void NextButton(){
        GameObject player = GameObject.Find("Bartender");
        player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        SceneManager.LoadScene("Forest");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
    }

    public void MapButton(){
        // GameObject player = GameObject.Find("Bartender");
        // player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        SceneManager.LoadScene("Map");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
    }

    public void MainMenuButton(){
        // GameObject player = GameObject.Find("Bartender");
        // player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        SceneManager.LoadScene("TitleScreen");
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
    }
}
