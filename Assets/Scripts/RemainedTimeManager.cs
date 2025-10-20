using System;  
using UnityEngine;
using TMPro;

public class RemainedTimeManager : MonoBehaviour
{
    public static string displayMessage = "a";
    public TextMeshProUGUI titleText;

    public float remainedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainedTime = 180;
    }

    // Update is called once per frame
    void Update()
    {
        remainedTime -= Time.deltaTime;
        int intRemainedTime = Mathf.CeilToInt(remainedTime);
        // int time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        // remainedTime = 180 - time;
        int remainedMinute = Mathf.FloorToInt(intRemainedTime / 60);
        int remainedSecond = intRemainedTime % 60;
        //秒数を二けた表示にする
        string s = remainedSecond.ToString("D2");
        displayMessage = (remainedMinute + ":" + s);
        titleText.text = displayMessage;
    }

    public void RemainedTimeDecrease(float decreaseAmount)
    {
        remainedTime -= decreaseAmount;
    }
}
