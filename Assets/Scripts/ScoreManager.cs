using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public double first_score = 12000;
    public double second_score = 8000;
    public double third_score = 4000;
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
        }
    }
    static public double score;
    IEnumerator CalcScore()
    {
        score = remainedTimeManager.remainedTime * 100;
        Debug.Log(score);
        yield break;
    }



    IEnumerator EvaluateScore(double score)
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
