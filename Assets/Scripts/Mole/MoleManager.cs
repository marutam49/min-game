using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//モグラのスポーン後の処理
public class MoleManager : MonoBehaviour
{
    public WaveManager waveManager;

    HitRangeManager hitRangeManager;

    Rigidbody2D rigidbody2D;
    WeaponManager weaponManager;

    [SerializeField]
    [Tooltip("発生させるエフェクト（パーティクル）")]
    private ParticleSystem particle1;
    [SerializeField]
    private ParticleSystem particle2;

    Renderer moleRenderer;
    SpriteRenderer spriteRenderer;

    RemainedTimeManager remainedTimeManager;
    ScreenShaker screenShaker;
    FeverGaugeController feverGaugeController;

    int hp = 10;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 3.0f;

    public int moleNumber;

    void Start()
    {
        remainedTimeManager = FindAnyObjectByType<RemainedTimeManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        moleRenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        screenShaker = FindAnyObjectByType<ScreenShaker>();
        feverGaugeController = FindAnyObjectByType<FeverGaugeController>();

        //Destroy(gameObject, despawnTime);
        //rigidbody2D.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        System.Random r = new System.Random();
        float moveSelect = (float)(r.NextDouble());

        Debug.Log(moleNumber);

        moleRenderer.sortingOrder = -moleNumber;

        if (moveSelect >= 0.5)
        {
            StartCoroutine(MoleMove1());
            rigidbody2D.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        }
        else
        {
            StartCoroutine(MoleMove2());
        }
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
            waveManager.enemyBeatNumber += 0.5f;
            LevelManager.exp += 5;
            WeaponManager.feverFlag += 1;
            Debug.Log(WeaponManager.feverFlag);
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
                //gameObject.GetComponent<Renderer>().material.color = Color.white;
                Destroy(collider.gameObject);
                ParticleSystem newParticle = Instantiate(particle1);
                newParticle.transform.position = this.transform.position;
                newParticle.Play();
                Destroy(newParticle.gameObject, 5.0f);
            }
        }
    }

    IEnumerator MoleMove1()
    {
        while (distanceFromCamera >= 1.0f)
        {
            transform.localScale = new Vector3(30 / distanceFromCamera, 30 / distanceFromCamera, 1);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - distanceFromCamera * 0.02f);
            yield return new WaitForSeconds(0.01f);
            distanceFromCamera -= 0.05f;
            Vector3 currentPosition = transform.position;
            //端で反転する
            if (currentPosition.x > 9 || -9 > currentPosition.x)
            {
                rigidbody2D.linearVelocityX = -rigidbody2D.linearVelocityX;
            }
            if (currentPosition.y > 5 || -5 > currentPosition.y)
            {
                rigidbody2D.linearVelocityY = -rigidbody2D.linearVelocityY;
            }
        }

        Destroy(this.gameObject);
        remainedTimeManager.RemainedTimeDecrease(1.0f);
        screenShaker.Shake();
    }
        IEnumerator MoleMove2()
    {
        Vector2 center = new Vector2(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f));
        float angleSpeed = Random.Range(-4f, 4f);
        float radius = Random.Range(1f, 3f);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        while (distanceFromCamera >= 1.0f)
        {
            transform.localScale = new Vector3(30 / distanceFromCamera, 30 / distanceFromCamera, 1);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - distanceFromCamera * 0.02f);
            angle += angleSpeed * Time.deltaTime;
            Vector2 pos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            transform.position = pos;
            yield return new WaitForSeconds(0.01f);
            distanceFromCamera -= 0.05f;
            Vector3 currentPosition = transform.position;
            //端で反転する
            if (currentPosition.x > 9 || -9 > currentPosition.x)
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
