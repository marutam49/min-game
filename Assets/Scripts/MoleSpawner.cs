using System.Collections;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.SocialPlatforms.Impl;

//モグラをスポーンさせる、モグラの種類と出現時のステータスの管理
public class MoleSpawner : MonoBehaviour
{

    public GameObject originObject;
    public GameObject secondObject;

    //waveごとの各種ステータス初期値
    //{enemyType, spawnInterval, numberSpawnAtOneTime}(enemyTypeは未実装)
    WaveState[] waveFirstState = new WaveState[]{
        new WaveState(
            EnemyType: 1,
            SpawnInterval: 1.0f,
            NumberSpawnAtOneTime: 1),
        new WaveState(
            EnemyType: 2,
            SpawnInterval: 0.5f,
            NumberSpawnAtOneTime: 2),
        new WaveState(
            EnemyType: 3,
            SpawnInterval: 0.3f,
            NumberSpawnAtOneTime: 2)
    };

    WaveState stateNow;

    void Start()
    {
        originObject.GetComponent<MoleManager>().waveManager = this.GetComponent<WaveManager>();
        WaveUpdate();
        StartCoroutine(Spawn());
    }


    void Update()
    {

    }


    IEnumerator Spawn()
    {
        while (WaveManager.wave == 1)
        {
            yield return new WaitForSeconds(stateNow.SpawnInterval);

            for (int i = 0; i < stateNow.NumberSpawnAtOneTime; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(originObject, spawnPoint, Quaternion.identity);
            }
        }
    
        while (WaveManager.wave >= 2)
        {
            yield return new WaitForSeconds(stateNow.SpawnInterval);

            for (int i = 0; i < stateNow.NumberSpawnAtOneTime; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(secondObject, spawnPoint, Quaternion.identity);
            }
        }
    }

    public void WaveUpdate()
    {
        stateNow = waveFirstState[WaveManager.wave - 1];
    }
}

public record WaveState(
    int EnemyType,
    float SpawnInterval,
    int NumberSpawnAtOneTime
);