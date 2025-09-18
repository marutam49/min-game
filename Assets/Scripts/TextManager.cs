using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject level_object = null;
    private Text level_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level_text = level_object.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int level = LevelManager.level;
        level_text.text = "Lv" + level;
    }
}
