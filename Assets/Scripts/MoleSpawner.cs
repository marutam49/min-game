using System.Collections;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.CompilerServices;

//モグラをスポーンさせる、モグラの種類と出現時のステータスの管理
public class MoleSpawner : MonoBehaviour
{

    public GameObject originObject;
    public GameObject secondObject;
    public GameObject thirdObject;
    public GameObject ForthObject;
    public GameObject FifthObject;
    public GameObject SixthObject;

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
            SpawnInterval: 0.7f,
            NumberSpawnAtOneTime: 1),
        new WaveState(
            EnemyType: 4,
            SpawnInterval: 0.3f,
            NumberSpawnAtOneTime: 1),
        new WaveState(
            EnemyType: 5,
            SpawnInterval: 0.6f,
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
        secondObject.GetComponent<Mole2Manager>().waveManager = this.GetComponent<WaveManager>();
        thirdObject.GetComponent<Mole3Manager>().waveManager = this.GetComponent<WaveManager>();
        ForthObject.GetComponent<Mole4Manager>().waveManager = this.GetComponent<WaveManager>();
        FifthObject.GetComponent<Mole5Manager>().waveManager = this.GetComponent<WaveManager>();
        SixthObject.GetComponent<Mole6Manager>().waveManager = this.GetComponent<WaveManager>();
        WaveUpdate();
        StartCoroutine(Spawn());
    }


    void Update()
    {

    }

    private bool wave2SpawnFlag = true;
    private bool wave4SpawnFlag = true;
    private bool wave6SpawnFlag = true;

    IEnumerator Spawn()
    {
        int spawnNumber = 0;//moleの前後関係用

        while (true)
        {
            yield return new WaitForSeconds(stateNow.SpawnInterval);
            if (WaveManager.wave == 1)
            {

                for (int i = 0; i < stateNow.NumberSpawnAtOneTime; i++)
                {
                    Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                    Instantiate(originObject, spawnPoint, Quaternion.identity);

                    originObject.GetComponent<MoleManager>().moleNumber = spawnNumber;
                    spawnNumber++;
                }
            }
            if (WaveManager.wave == 2 && wave2SpawnFlag)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(secondObject, spawnPoint, Quaternion.identity);
                wave2SpawnFlag = false;

                secondObject.GetComponent<Mole2Manager>().moleNumber = spawnNumber;
                spawnNumber++;
            }
            if (WaveManager.wave == 3)
            {
                for (int i = 0; i < stateNow.NumberSpawnAtOneTime; i++)
                {
                    Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                    Instantiate(thirdObject, spawnPoint, Quaternion.identity);

                    thirdObject.GetComponent<Mole3Manager>().moleNumber = spawnNumber;
                    spawnNumber++;
                }
            }
            if (WaveManager.wave == 4 && wave4SpawnFlag)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(ForthObject, spawnPoint, Quaternion.identity);
                wave4SpawnFlag = false;

                ForthObject.GetComponent<Mole4Manager>().moleNumber = spawnNumber;
                spawnNumber++;
            }
            if (WaveManager.wave == 5)
            {
                for (int i = 0; i < stateNow.NumberSpawnAtOneTime; i++)
                {
                    Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                    Instantiate(FifthObject, spawnPoint, Quaternion.identity);

                    FifthObject.GetComponent<Mole5Manager>().moleNumber = spawnNumber;
                    spawnNumber++;
                }
            }
            if (WaveManager.wave == 6 && wave6SpawnFlag)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(SixthObject, spawnPoint, Quaternion.identity);
                wave6SpawnFlag = false;

                SixthObject.GetComponent<Mole6Manager>().moleNumber = spawnNumber;
                spawnNumber++;
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