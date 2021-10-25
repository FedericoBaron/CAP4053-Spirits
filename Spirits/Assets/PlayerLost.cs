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
            score = player.GetComponent<Player_Combat>().money;
            Destroy(player);
        }
        moneyText.text = "Money: " + score.ToString();
    }

    public void RestartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
