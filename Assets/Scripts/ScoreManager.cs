using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//スコアUIの表示
public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreDisplay;
    RemainedTimeManager remainedTimeManager;


    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        remainedTimeManager = FindAnyObjectByType<RemainedTimeManager>();
    }
    bool gameClearFlag = false;
    //適当な値に上位スコアを設定
    static public int first_score = 12000;
    static public int second_score = 8000;
    static public int third_score = 4000;
    void Update()
    {
        //scoreDisplay.text = //"score:" + score.ToString();
        //"level:" + LevelManager.level.ToString();
        if (WaveManager.wave > 6 && gameClearFlag == false)
        {
             SceneManager.LoadScene("Result");
            Debug.Log("くぁｗせｄｒｆｔｇｙふじこｌｐ");
            gameClearFlag = true;
            StartCoroutine(CalcScore());
            StartCoroutine(EvaluateScore(score));
            Debug.Log(first_score);
            Debug.Log(second_score);
            Debug.Log(third_score);
        }
    }
    static public int score;
    IEnumerator CalcScore()
    {
        float scorefloat = remainedTimeManager.remainedTime * 100;
        score = (int) Math.Ceiling(scorefloat);
        Debug.Log(score);
        yield break;
    }



    IEnumerator EvaluateScore(int score)
    {
        if (score > first_score)
        {
            third_score = second_score;
            second_score = first_score;
            first_score = score;
        }
        else if (score > second_score)
        {
            third_score = second_score;
            second_score = score;
        }
        else if (score > third_score)
        {
            third_score = score;
        }
        yield break;
    }
}
