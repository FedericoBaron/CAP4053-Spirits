using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    static int amount;
    static int currAmount;
    

    // Start is called before the first frame update
    void Start()
    {
        Money.amount = 0;
        Money.currAmount = 0;
    }

    public void addMoney(int amount){
        Money.amount += amount;
    }

    public void subtractMoney(int amount){
        Money.amount -= amount;
    }

    public void addCurrMoney(int amount){
        Money.currAmount += amount;
        updateCurrMoney();
    }

    public void subtractCurrMoney(int amount){
        Money.currAmount -= amount;
        updateCurrMoney();
    }

    public void updateCurrMoney(){
        Debug.Log("update moneyyyy");
        // MoneyTextManager.instance.setText(Money.currAmount);
    }

    public int getMoney(){
        return Money.amount;
    }
        
    public int getCurrMoney(){
        return Money.currAmount;
    }
}
