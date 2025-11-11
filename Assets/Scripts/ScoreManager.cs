using UnityEngine;
using TMPro;
using System.Collections;

//スコアUIの表示
public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreDisplay;
    RemainedTimeManager remainedTimeManager;
    public static int score = 0;

    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        remainedTimeManager = FindAnyObjectByType<RemainedTimeManager>();
    }


    void Update()
    {
        bool gameClearFlag = true;
        scoreDisplay.text = //"score:" + score.ToString()
        "level:" + LevelManager.level.ToString();
        if (WaveManager.wave > 6 && gameClearFlag == true)
        {
            gameClearFlag = false;
            StartCoroutine(CalcScore());
        }
    }
    
    IEnumerator CalcScore()
    {
        yield break;
        float Score = remainedTimeManager.remainedTime;
        Debug.Log(Score);
    }
}
