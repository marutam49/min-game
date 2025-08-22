using System.Collections;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.SocialPlatforms.Impl;

//モグラをスポーンさせる
public class MoleSpawner : MonoBehaviour
{

    public GameObject originObject;

    //waveごとの各種ステータス初期値
    //{enemyTipe, spawnInterval, numberSpawnAtOneTime}(enemyTipeは未実装)
    float[,] waveFirstState = new float[3, 3]{
        {1, 1, 1},
        {2, 0.1f, 1},
        {3, 1, 1}
    };

    float spawnInterval;

    int numberSpawnAtOneTime;

    void Start()
    {
        WaveUpdate();
        StartCoroutine(Spawn());
    }


    void Update()
    {

    }


    IEnumerator Spawn()
    {
        while (true)
        {
            //spawnInterval = 1.0f / (ScoreManager.score / 20 + 1);
            //numberSpawnAtOneTime = 1;

            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < numberSpawnAtOneTime; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(originObject, spawnPoint, Quaternion.identity);
            }
        }
    }

    public void WaveUpdate()
    {
        spawnInterval = waveFirstState[WaveManager.wave - 1, 1];
        numberSpawnAtOneTime = (int)waveFirstState[WaveManager.wave - 1, 2];        
    }
}
