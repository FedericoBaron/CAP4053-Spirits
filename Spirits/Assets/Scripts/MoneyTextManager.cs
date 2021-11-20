using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTextManager : MonoBehaviour
{
    public static MoneyTextManager instance;

    //public Text moneyText;
    public int money;

    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // moneyText.text = "Money: 0";
        money = 0;
    }

    public void setText(int amount){
        //Debug.Log("SET TEXT");
        money += amount;
       // moneyText.text = "Money: " + money;
    }
}
