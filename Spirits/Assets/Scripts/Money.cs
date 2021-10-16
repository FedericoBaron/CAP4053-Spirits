using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    static int amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Money.amount = 0;
    }

    public void addMoney(int amount){
        Money.amount += amount;
    }

    public int getMoney(){
        return Money.amount;
    }
}
