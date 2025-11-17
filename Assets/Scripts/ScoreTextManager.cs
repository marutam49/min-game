using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using System;

public class ScoreTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testText;
    void Start()
    {
        int scoreint = (int)Math.Ceiling(ScoreManager.score);
        string scoreText = scoreint.ToString();
        Debug.Log(scoreText);
        testText.SetText(scoreText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
