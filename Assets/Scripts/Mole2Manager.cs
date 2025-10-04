using System.Collections;
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


    int hp = 30;

    //float despawnTime = 3.0f;

    public float distanceFromCamera = 10.0f;


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
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 movePoint = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                movePoint = Camera.main.ViewportToWorldPoint(movePoint);
                transform.position = movePoint;
                yield return new WaitForSeconds(2.0f);
            }

            Destroy(this.gameObject);
        }
            // while (distanceFromCamera >= 1.0f)
        // {
        //     transform.localScale = new Vector3(5 / distanceFromCamera, 5 / distanceFromCamera, 1);
        //     yield return new WaitForSeconds(0.01f);
        //     distanceFromCamera -= 0.05f;
        // }

        // Destroy(this.gameObject);


    }
}
