using System;
using UnityEngine;
using Random = UnityEngine.Random;

//モグラのスポーン後の処理
public class MoleManager : MonoBehaviour
{
    bool isItem = false;

    float despawnTime = 3.0f;
    
    void Start()
    {
        //アイテムかどうかの判定
        if (Random.Range(0, 2) == 1)
        {
            isItem = true;
        }
        if (isItem)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }

        Destroy(gameObject, despawnTime);
    }

    
    void Update()
    {
        HitRangeManager hitRangeManager = FindAnyObjectByType<HitRangeManager>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -9;

        //クリックされたかの判定
        if (hitRangeManager.doHitDecision)
        {
            if ((mousePos - transform.position).magnitude * 2 <= transform.localScale.x + hitRangeManager.hitRange)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                ScoreManager.score += 5;
                Destroy(gameObject, 0.1f);

                if (isItem)
                {
                    hitRangeManager.hitRange += 0.01f;
                    hitRangeManager.firirngInterval -= 0.01f;
                }

                enabled = false;
            }
        }
    }
}
