using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] HitRangeManager hitRangeManager;

    BulletManager bulletManager;
    WaveManager waveManager;
    [SerializeField]
    private ParticleSystem feverParticle;

    [SerializeField] WeaponShowManager weaponShowManager;

    public int playerWeapon = 0;
    public static int feverFlag = 0;

    //{hitRange, firingInterval, attack, bulletSpeed}(bulletSpeedは未実装)
    WeaponState[] weaponFirstState = new WeaponState[]{
        new WeaponState(
            HitRange: 0.3f,
            FiringInterval: 0.2f,
            Attack: 10,
            BulletSpeed: 0.25f
        ),
        new WeaponState(
            HitRange: 0.1f,
            FiringInterval: 0.10f,
            Attack: 10,
            BulletSpeed: 0.4f
        ),
        new WeaponState(
            HitRange: 1.5f,
            FiringInterval: 1f,
            Attack: 10,
            BulletSpeed: 0.10f
        )
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletManager = FindAnyObjectByType<BulletManager>();
        WeaponReset();
        StartCoroutine(feverTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WeaponReset()
    {
        if (weaponShowManager.selectWeaponNumber < 1 || weaponShowManager.selectWeaponNumber > 3)
        {
            weaponShowManager.selectWeaponNumber = 1;
        }
        hitRangeManager.weaponState = weaponFirstState[weaponShowManager.selectWeaponNumber - 1];
    }

    public static int feverCount;
    private IEnumerator feverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            feverCount += 1;

            if (feverCount >= 2000)
            {
                feverCount = 0;
                feverFlag = 0;
            }
            else
            {
                if (feverFlag >= 5)
                {
                    var state = hitRangeManager.weaponState;
                    hitRangeManager.weaponState = new WeaponState(
                    HitRange: state.HitRange,
                    FiringInterval: 0.01f,
                    Attack: state.Attack,
                    BulletSpeed: state.BulletSpeed
                    );
                    ParticleSystem feverInstance = Instantiate(feverParticle);
                    var feverMat = feverInstance.GetComponent<ParticleSystemRenderer>().material;
                    feverMat.renderQueue = 3000;
                    feverInstance.transform.position = new Vector3(0,0,0);
                    feverInstance.Play();
                    yield return new WaitForSeconds(5.0f);
                    hitRangeManager.weaponState = new WeaponState(
                    HitRange: state.HitRange,
                    FiringInterval: 0.2f,
                    Attack: state.Attack,
                    BulletSpeed: state.BulletSpeed
                    );
                    feverFlag = 0;
                }
            }
        }
    }
}



public record WeaponState(
    float HitRange,
    float FiringInterval,
    int Attack,
    float BulletSpeed
);

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit
    {
        
    }
}
