using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSummary : MonoBehaviour
{
    public Text Money;

    public void Start(){
        GameObject player = GameObject.Find("Bartender");
        int score = -1;
        if (player != null){
            score = player.GetComponent<Player_Combat>().money;
            Destroy(player);
        }
        Money.text = "Money: " + score.ToString();
    }

    public void RestartButton(){
        SceneManager.LoadScene("Tutorial");
    }

    public void NextButton(){
        SceneManager.LoadScene("Forest");
    }
}
