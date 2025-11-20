using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class reset_script : MonoBehaviour
{
    private float counttime = 0.0f;
    public float timeLimit = 30.0f;
    void Start()
    {
        
    }

    void Update()
    {
        counttime += Time.deltaTime;


        if(counttime > timeLimit)
        {
            SceneManager.LoadScene("Title");
        }

    }
}