using System;
using UnityEngine;

//wave切り替えの管理
public class WaveManager : MonoBehaviour
{
    [SerializeField] private MoleSpawner moleSpawner;

    public static int wave = 1;
    public int enemyBeatNumber = 0;
    public int waveEnemyBeatQuota = 1;

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
        waveTextManager.changeText("WAVE:"+wave);
    }

}
