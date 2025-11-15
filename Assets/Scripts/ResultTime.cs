using UnityEngine;
using TMPro;
public class ResultLevelText : MonoBehaviour
{
    private TextMeshProUGUI ResultTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResultTime = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        string Time = RemainedTimeManager.displayMessage;
        ResultTime.text = Time;
        
    }
}
