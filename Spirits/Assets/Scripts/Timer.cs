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
    public Image circleMeter;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;

    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = 0;
        
        if(timerActive){
            currentTime = currentTime - Time.deltaTime;
            elapsedTime = Time.deltaTime / (60 * startMinutes);
            if(currentTime <= 0){
                timerActive = false;
                Debug.Log("Timer finished");
                currentTime = 0;
                elapsedTime = 0;
            }
        }
        
        circleMeter.GetComponent<Image>().fillAmount += elapsedTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = format(time);
    }

    string format(TimeSpan time){
        string mins = "0";
        if (time.Minutes >= 10)
            mins = time.Minutes.ToString();
        else
            mins += time.Minutes.ToString();
        
        string sec = "0";
        if (time.Seconds >= 10)
            sec = time.Seconds.ToString();
        else
            sec += time.Seconds.ToString();

        return mins + ":" + sec;
    }
    
}
