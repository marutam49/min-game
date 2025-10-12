using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Mole2Manager : MonoBehaviour
{
    public WaveManager waveManager;

    HitRangeManager hitRangeManager;

    Rigidbody2D rigidbody2D;

    [SerializeField]
    [Tooltip("発生させるエフェクト（パーティクル）")]
    private ParticleSystem particle1;
    [SerializeField]
    private ParticleSystem particle2;
    [SerializeField]
    private ParticleSystem warpParticle;
    [SerializeField]
    private ParticleSystem warpParticle_out;


    int hp = 50;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 5.0f;
    private float alpha = 1f;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        StartCoroutine(MoleMove());
    }


    void Update()
    {
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
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (moveSelect < 0.25)
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
                    }
                }
                alpha = 1f;
                c.a = alpha;
                sr.color = c;
                transform.position = movePoint;
                transform.localScale = new Vector3(30 / distanceFromCamera, 30 / distanceFromCamera, 1);
                float valuableNumber = (float)(r.NextDouble() * 0.2 - 0.1);
                distanceFromCamera += valuableNumber;
            }
            if (moveSelect >= 0.25)
            {
                Vector3 currentPosition = transform.position;
                Vector2 moveDirection = Vector2.zero;
                //いる方向と逆方向に移動
                float speedX = (float)(r.NextDouble() * 20 + 20);
                if (currentPosition.x > 0)
                    speedX = -speedX;
                moveDirection.x = speedX;
                float speedY = (float)(r.NextDouble() * 20 + 20);
                if (currentPosition.y > 0)
                    speedY = -speedY;
                moveDirection.y = speedY;
                for (int i = 0; i < 26; i++)
                {
                    transform.position += new Vector3(moveDirection.x, moveDirection.y, 0f) * Time.deltaTime;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
