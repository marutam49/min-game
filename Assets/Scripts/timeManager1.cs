using System;  
using UnityEngine;
using TMPro;

public class timeManager1 : MonoBehaviour
{
    public static string displayMessage = "a";
    public TextMeshProUGUI titleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        int remainedTime = 180 - time;
        int remainedMinute = Mathf.FloorToInt(remainedTime / 60);
        int remainedSecond = remainedTime % 60;
        //秒数を二けた表示にする
        string s = remainedSecond.ToString("D2");
        displayMessage = (remainedMinute + ":" + s);
        titleText.text = displayMessage;
    }
}
