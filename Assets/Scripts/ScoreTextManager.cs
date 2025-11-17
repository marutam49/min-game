using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using System;

public class ScoreTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private TextMeshProUGUI rank1Text;
    [SerializeField] private TextMeshProUGUI rank2Text;
    [SerializeField] private TextMeshProUGUI rank3Text;
    ScoreManager scoreManager;
    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        int scoreint = (int)Math.Ceiling(ScoreManager.score);
        string scoreText = scoreint.ToString();
        /*string firstScoreText = scoreManager.first_score.ToString();
        string secondScoreText = scoreManager.second_score.ToString();
        string thirdScoreText = scoreManager.third_score.ToString();
        rank1Text.SetText($"1:{firstScoreText}");
        rank2Text.SetText($"2:{secondScoreText}");
        rank3Text.SetText($"3:{thirdScoreText}");(ぬるぽになっちゃうから一旦コメントアウト)*/
        testText.SetText(scoreText);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
