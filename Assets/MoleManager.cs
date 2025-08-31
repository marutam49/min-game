using System;
using UnityEngine;
using Random = UnityEngine.Random;

//モグラのスポーン後の処理
public class MoleManager : MonoBehaviour
{
    public WaveManager waveManager;

    float despawnTime = 3.0f;

    HitRangeManager hitRangeManager;

    void Start()
    {
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        Destroy(gameObject, despawnTime);
    }

    
    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -9;

        //クリックされたかの判定
        if (hitRangeManager.doHitDecision)
        {
            if ((mousePos - transform.position).magnitude * 2 <= transform.localScale.x + hitRangeManager.hitRange)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                ScoreManager.score += 5;
                waveManager.enemyBeatNumber += 1;
                LevelManager.exp += 5;

                if (waveManager.enemyBeatNumber >= waveManager.waveEnemyBeatQuota)
                {
                    waveManager.WaveAdd();
                }
                
                Destroy(gameObject, 0.1f);

                enabled = false;
            }
        }
    }
}
