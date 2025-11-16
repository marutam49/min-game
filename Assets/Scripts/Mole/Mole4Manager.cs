using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//モグラのスポーン後の処理
public class Mole4Manager : MonoBehaviour
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
    [SerializeField]
    private ParticleSystem warpParticle;
    [SerializeField]
    private ParticleSystem warpParticle_out;
    Renderer mole4Renderer;
    [SerializeField] GameObject Mole4Bullet;
    SpriteRenderer sr;

    int hp = 500;
    private float alpha = 1.0f;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 3.0f;

    public int moleNumber;



    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        mole4Renderer = GetComponent<Renderer>();
        sr = GetComponent<SpriteRenderer>();
        mole4Renderer.sortingOrder = -moleNumber;
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
            waveManager.enemyBeatNumber += 1;
            LevelManager.exp += 5;
            ParticleSystem newParticle = Instantiate(particle2);
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 5.0f);
            Destroy(gameObject, 0.1f);
            waveManager.WaveAdd();

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



        IEnumerator Warp()
        {
            ParticleSystem newParticle = Instantiate(warpParticle);
            //effectsがmoleより前に配置されるようにする。
            var mat = newParticle.GetComponent<ParticleSystemRenderer>().material;
            mat.renderQueue = 3100;
            Color c = sr.material.color;
            newParticle.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            newParticle.Play();
            Vector3 movePoint = new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
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
                        var mat_2 = newParticle_2.GetComponent<ParticleSystemRenderer>().material;
                        mat_2.renderQueue = 3100;
                        newParticle_2.transform.position = movePoint;
                        newParticle_2.Play();
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
                float speedX = (float)(r.NextDouble() * 15 + 5);
                if (currentPosition.x > 0)
                    speedX = -speedX;
                moveDirection.x = speedX;
                float speedY = (float)(r.NextDouble() * 15 + 5);
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
            for (int i = 0; i < attackFrequency; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(Mole4Bullet, transform.position, Quaternion.identity);
            }
        }
    }
}

