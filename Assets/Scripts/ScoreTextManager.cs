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
    void Start()
    {
        string scoreText = ScoreManager.score.ToString();
        string firstScoreText = ScoreManager.first_score.ToString();
        string secondScoreText = ScoreManager.second_score.ToString();
        string thirdScoreText = ScoreManager.third_score.ToString();
        rank1Text.SetText($"1:{firstScoreText}");
        rank2Text.SetText($"2:{secondScoreText}");
        rank3Text.SetText($"3:{thirdScoreText}");
        testText.SetText(scoreText);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
