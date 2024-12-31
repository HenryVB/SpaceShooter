using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private TextMeshProUGUI txtTime;

    private float startTime;
    private float ellapsedTime;
    private bool startCounter;
    private int timeInMinutes;
    private int timeInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        startCounter = false;
        txtTime = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounter)
        {
            //Calculate
            ellapsedTime = Time.time - startTime;
            timeInMinutes = (int) ellapsedTime / 60;
            timeInSeconds = (int)ellapsedTime % 60;

            txtTime.text = string.Format("{0:00}:{1:00}",timeInMinutes,timeInSeconds);
        }
    }

    public void StartTimeCounter() {
        startTime = Time.time;
        startCounter = true;
    }

    public void StopTimeCounter()
    {
        startCounter = false;
    }

}
