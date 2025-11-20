using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Mole2Manager : MonoBehaviour
{
    public WaveManager waveManager;

    HitRangeManager hitRangeManager;
    WeaponManager weaponManager;

    Rigidbody2D rigidbody2D;
    ScreenShaker screenShaker;
    RemainedTimeManager remainedTimeManager;

    [SerializeField]
    [Tooltip("発生させるエフェクト（パーティクル）")]
    private ParticleSystem particle1;
    [SerializeField] private ParticleSystem particle2;
    [SerializeField] private ParticleSystem warpParticle;
    [SerializeField] private ParticleSystem warpParticle_out;
    [SerializeField] private ParticleSystem attackMotion; 


    Renderer mole2Renderer;

    SpriteRenderer sr;
    [SerializeField] GameObject Mole2Bullet;


    int hp = 75;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 0.1f;
    private float alpha = 1f;

    public int moleNumber;



    void Start()
    {
        remainedTimeManager = FindAnyObjectByType<RemainedTimeManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        mole2Renderer = GetComponent<Renderer>();
        sr = GetComponent<SpriteRenderer>();
        screenShaker = FindAnyObjectByType<ScreenShaker>();

        mole2Renderer.sortingOrder = -moleNumber;
        StartCoroutine(MoleMove());
    }


    void Update()
    {
        if (hp <= 0)
        {
            StopAllCoroutines();
            rigidbody2D.simulated = false; 

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            //ScoreManager.core += 5;
            waveManager.enemyBeatNumber += 1;
            LevelManager.exp += 5;
            ParticleSystem newParticle = Instantiate(particle2);
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 5.0f);

            waveManager.WaveAdd();

            Destroy(gameObject, 0.1f);

            enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            BulletManager bulletManager = collider.gameObject.GetComponent<BulletManager>();
            if (-0.4f < bulletManager.distanceFromCamera - distanceFromCamera && bulletManager.distanceFromCamera - distanceFromCamera < 0.4f)
            {
                hp -= hitRangeManager.weaponState.Attack;
                WeaponManager.feverFlag += 1;
                Destroy(collider.gameObject);
                // ParticleSystem newParticle = Instantiate(particle1);
                // newParticle.transform.position = this.transform.position;
                // newParticle.Play();
                // Destroy(newParticle.gameObject, 5.0f);

                ParticleSystem newParticle = Instantiate(particle1);
                var r = newParticle.GetComponent<ParticleSystemRenderer>();
                r.material = new Material(r.sharedMaterial); // 複製
                newParticle.transform.position = this.transform.position;
                newParticle.Play();
                Destroy(newParticle.gameObject, 5.0f);

                        //remainedTimeManager.RemainedTimeDecrease(1.0f);
                screenShaker.Shake();
                remainedTimeManager.remainedTime -= 1;

            }
        }
    }

    IEnumerator MoleMove()
    {
        System.Random r = new System.Random();
        while (distanceFromCamera >= 1.0f)
        {
            float moveSelect = (float)(r.NextDouble());

            if (moveSelect <= 0.25)
            {
                yield return StartCoroutine(Warp());
            }
            else if (moveSelect <= 0.90)
            {
                yield return StartCoroutine(Move());
            }
            else
            {
                yield return StartCoroutine(Attack());
            }
        }
        // while (true)
        // {
        //     yield return new WaitForSeconds(0.01f);
        //     Vector3 currentPosition = transform.position;
        //     //端で反転する
        //     if (currentPosition.x > 9 || -9 > currentPosition.x)
        //     {
        //         rigidbody2D.linearVelocityX = -rigidbody2D.linearVelocityX;
        //     }
        //     if (currentPosition.y > 5 || -5 > currentPosition.y)
        //     {
        //         rigidbody2D.linearVelocityY = -rigidbody2D.linearVelocityY;
        //     }
        // }

        IEnumerator Warp()
        {
            ParticleSystem newParticle = Instantiate(warpParticle);
            //effectsがmoleより前に配置されるようにする。
            //var mat = newParticle.GetComponent<ParticleSystemRenderer>().material;
            //mat.renderQueue = 3100;

            var renderer = newParticle.GetComponent<ParticleSystemRenderer>();
            renderer.material = new Material(renderer.material);
            renderer.material.renderQueue = 3100;

            Color c = sr.color;
            newParticle.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            newParticle.Play();

            Destroy(newParticle.gameObject, 5.0f);

            Vector3 movePoint = new Vector3(UnityEngine.Random.Range(0.0f, 0.8f), UnityEngine.Random.Range(0.0f, 0.8f), 1.0f);
            movePoint = Camera.main.ViewportToWorldPoint(movePoint);
            for (int i = 0; i < 20; i++)
            {
                alpha -= 0.05f;
                alpha = Mathf.Clamp01(alpha);
                c.a = alpha;
                sr.color = c;
                yield return new WaitForSeconds(0.065f);
                if (i == 12)
                {
                    ParticleSystem newParticle_2 = Instantiate(warpParticle_out);

                    // var mat_2 = newParticle_2.GetComponent<ParticleSystemRenderer>().material;
                    // mat_2.renderQueue = 3100;
                    // newParticle_2.transform.position = movePoint;
                    // newParticle_2.Play();
                    var psr2 = newParticle_2.GetComponent<ParticleSystemRenderer>();
                    psr2.material = new Material(psr2.sharedMaterial); // ここも複製
                    psr2.material.renderQueue = 3100;
                    newParticle_2.transform.position = movePoint;
                    newParticle_2.Play();
                    Destroy(newParticle_2.gameObject, 5.0f); // 忘れずDestroy

                    transform.position = movePoint;
                    transform.localScale = new Vector3(30 / distanceFromCamera, 30 / distanceFromCamera, 1);
                    float valuableNumber = (float)(r.NextDouble() * 0.2 - 0.1);
                    distanceFromCamera += valuableNumber;
                }
            }
            alpha = 1f;
            c.a = alpha;
            sr.color = c;
        }

        IEnumerator Move()
        {
            Vector3 currentPosition = transform.position;
            Vector2 moveDirection = Vector2.zero;
            //いる方向と逆方向に移動
            float speedX = (float)(r.NextDouble() * 10 + 7);
            if (currentPosition.x > 0)
                speedX = -speedX;
            moveDirection.x = speedX;
            float speedY = (float)(r.NextDouble() * 10 + 7);
            if (currentPosition.y > 0)
                speedY = -speedY;
            moveDirection.y = speedY;
            for (int i = 0; i < 26; i++)
            {
                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime;
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator Attack(int attackFrequency = 3)
        {
            ParticleSystem newParticle = Instantiate(attackMotion);
            var renderer = newParticle.GetComponent<ParticleSystemRenderer>();
            renderer.material = new Material(renderer.material);
            renderer.material.renderQueue = 2900;
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle,1.5f);
            for (int i = 0; i < attackFrequency; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(Mole2Bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
