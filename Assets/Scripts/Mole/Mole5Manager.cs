using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//モグラのスポーン後の処理
public class Mole5Manager : MonoBehaviour
{
    public WaveManager waveManager;

    HitRangeManager hitRangeManager;

    Rigidbody2D rigidbody2D;
    WeaponManager weaponManager;
    ScreenShaker screenShaker;
    RemainedTimeManager remainedTimeManager;

    [SerializeField]
    [Tooltip("発生させるエフェクト（パーティクル）")]
    private ParticleSystem particle1;
    [SerializeField]
    private ParticleSystem particle2;

    Renderer mole5Renderer;


    int hp = 10;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 3.0f;

    public int moleNumber;


    void Start()
    {
        remainedTimeManager = FindAnyObjectByType<RemainedTimeManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        mole5Renderer = GetComponent<Renderer>();
        screenShaker = FindAnyObjectByType<ScreenShaker>();
        mole5Renderer.sortingOrder = -moleNumber;
        //Destroy(gameObject, despawnTime);
        rigidbody2D.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        StartCoroutine(MoleMove());
    }


    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -9;

        //クリックされたかの判定
        // if (hitRangeManager.doHitDecision)
        // {
        //     if ((mousePos - transform.position).magnitude * 2 <= transform.localScale.x + hitRangeManager.hitRange)
        //     {
        //         hp -= hitRangeManager.attack;
        //     }
        // }


        if (hp <= 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            ScoreManager.score += 5;
            waveManager.enemyBeatNumber += 5.0f;//0.2f;
            LevelManager.exp += 5;
            ParticleSystem newParticle = Instantiate(particle2);
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 5.0f);

            if (waveManager.enemyBeatNumber >= waveManager.waveEnemyBeatQuota)
            {
                waveManager.WaveAdd();
            }

            Destroy(gameObject, 0.1f);

            enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < 3.0f / distanceFromCamera)
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                hp -= hitRangeManager.weaponState.Attack;
                WeaponManager.feverFlag += 1;
                //gameObject.GetComponent<Renderer>().material.color = Color.white;
                Destroy(collider.gameObject);
                ParticleSystem newParticle = Instantiate(particle1);
                newParticle.transform.position = this.transform.position;
                newParticle.Play();
                Destroy(newParticle.gameObject, 5.0f);
            }
        }
    }

    IEnumerator MoleMove()
    {
        while (distanceFromCamera >= 1.0f)
        {
            transform.localScale = new Vector3(5 / distanceFromCamera, 5 / distanceFromCamera, 1);
            yield return new WaitForSeconds(0.01f);
            distanceFromCamera -= 0.05f;
            Vector3 currentPosition = transform.position;
            //端で反転する
            if (currentPosition.x > 10 || -15 > currentPosition.x)
            {
                rigidbody2D.linearVelocityX = -rigidbody2D.linearVelocityX;
            }
            if (currentPosition.y > 5 || -5 > currentPosition.y)
            {
            rigidbody2D.linearVelocityY = -rigidbody2D.linearVelocityY;
            }
        }

        Destroy(this.gameObject);

        //remainedTimeManager.RemainedTimeDecrease(1.0f);
        screenShaker.Shake();
        remainedTimeManager.remainedTime -= 1;
    }
}
