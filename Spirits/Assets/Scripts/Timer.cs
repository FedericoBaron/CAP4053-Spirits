using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

    float currentTime;
    bool timerActive = true;
    public int startMinutes;
    public Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;

    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive){
            currentTime = currentTime - Time.deltaTime;
            if(currentTime <= 0){
                timerActive = false;
                Debug.Log("Timer finished");
                currentTime = 0;
            }
        }
        
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }
}
