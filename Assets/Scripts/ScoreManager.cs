using UnityEngine;
using TMPro;

//スコアUIの表示
public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreDisplay;
    public static int score = 0;

    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        scoreDisplay.text = "score:" + score.ToString()
                            + "level:" + LevelManager.level.ToString();
    }
}
