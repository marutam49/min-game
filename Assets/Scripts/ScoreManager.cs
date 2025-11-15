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
        bool gameClearFlag = false;
        scoreDisplay.text = //"score:" + score.ToString();
        "level:" + LevelManager.level.ToString();
        if (WaveManager.wave > 6 && gameClearFlag == false)
        {
            Debug.Log("くぁｗせｄｒｆｔｇｙふじこｌｐ");
            gameClearFlag = true;
            StartCoroutine(CalcScore());
            StartCoroutine(EvaluateScore(score));
        }
    }

    public float Score;
    IEnumerator CalcScore()
    {
        Score = remainedTimeManager.remainedTime;
        Debug.Log(Score);
        yield break;
    }

    //適当な値に上位スコアを設定
    float first_score = 200;
    float second_score = 150;
    float third_score = 100;

    IEnumerator EvaluateScore(float score)
    {
        if (score > first_score)
        {
            third_score = second_score;
            second_score = first_score;
            first_score = score;
        }
        if (score > second_score)
        {
            third_score = second_score;
            second_score = score;
        }
        if (score > third_score)
        {
            third_score = score;
        }
        yield break;
    }
}
