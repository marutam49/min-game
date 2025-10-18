using System;
using UnityEngine;

//wave切り替えの管理
public class WaveManager : MonoBehaviour
{
    [SerializeField] private MoleSpawner moleSpawner;

    public static int wave = 1;
    public float enemyBeatNumber = 0;
    public float waveEnemyBeatQuota = 1.0f;

    void Start()
    {

    }


    void Update()
    {

    }


    public void WaveAdd()
    {
        wave += 1;
        Debug.Log(wave);
        moleSpawner.WaveUpdate();
        enemyBeatNumber = 0;
        if(wave % 2 == 1)
            waveTextManager.changeText("WAVE:" + ((wave + 1) / 2));
    }
    
}
