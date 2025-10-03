using UnityEngine;
using TMPro;
public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI LevelDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelDisplay = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        int level = LevelManager.level;
        LevelDisplay.text = "Level:" + level;
        
    }
}
