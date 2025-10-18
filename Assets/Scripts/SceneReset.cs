using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class reset_script : MonoBehaviour
{
    private float counttime = 0.0f;//時間をはかる
    public float timeLimit = 30.0f;//制限時間

    void Start()
    {
        
    }

    void Update()
    {
        counttime += Time.deltaTime;//マイフレーム事にかかった時間を足している


        if(counttime > timeLimit)
        {
            SceneManager.LoadScene("Title");//指定した時間が過ぎたらシーン遷移。("")の中に遷移先のシーンの名前をいれる。
        }

    }
}