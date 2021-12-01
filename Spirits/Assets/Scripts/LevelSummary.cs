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
        Money.text = "Money: " + moneyMade.ToString();
        Spirits.text = "Spirits Captured: " + ghostsCaptured.ToString();
    }

    public void RestartButton(){
        SceneManager.LoadScene("Tutorial");
    }

    public void NextButton(){
        GameObject player = GameObject.Find("Bartender");
        player.GetComponent<Player_Combat>().health = player.GetComponent<Player_Combat>().maxHealth;  
        SceneManager.LoadScene("Forest");
    }
}
