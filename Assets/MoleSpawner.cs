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

    void Start()
    {
        StartCoroutine(Spawn());
    }

    
    void Update()
    {

    }
    

    IEnumerator Spawn()
    {
        while (true)
        {
            float spawnInterval = 1.0f / (ScoreManager.score / 20 + 1);
            int numberSpawnAtOneTime = 1;

            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < numberSpawnAtOneTime; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
                Instantiate(originObject, spawnPoint, Quaternion.identity);
            }
        }
    }
}
