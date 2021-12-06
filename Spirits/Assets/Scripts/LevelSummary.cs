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

    private void Start(){
        GameObject player = GameObject.Find("Bartender");
        int ghostsCaptured = -1;
        
        Money = GameObject.Find("Money").GetComponent<Text>();
        Spirits = GameObject.Find("Spirits").GetComponent<Text>();
        Recipes = GameObject.Find("Recipes").GetComponent<Text>();
        TotalMoney = GameObject.Find("TotalMoney").GetComponent<Text>();
        
        if (player != null){
            
            ghostsCaptured = player.GetComponent<Player_Combat>().ghostsCaptured;
            // Destroy(player);
            int recipesMade = Player_Combat.recipesMade;
            int moneyMade = (ghostsCaptured * 30) + (recipesMade * 10);
            int totalMoney = player.GetComponent<Player_Combat>().totalMoney + moneyMade;
            Debug.Log("This is money made: " + moneyMade.ToString());
            // set total money
            // if (TotalMoney != null && Money != null && Recipes != null && Spirits != null){
                Money.text = "Money: " + moneyMade.ToString();
                Recipes.text = "Recipes: " + recipesMade.ToString();
                TotalMoney.text = "Total Money: " + totalMoney.ToString();
                Spirits.text = "Spirits Captured: " + ghostsCaptured.ToString();
            // }
            player.GetComponent<Player_Combat>().ghostsCaptured = 0;
            player.GetComponent<Player_Combat>().totalMoney = totalMoney;
            Player_Combat.recipesMade = 0;
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
