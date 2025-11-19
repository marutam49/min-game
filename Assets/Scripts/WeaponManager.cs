using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] HitRangeManager hitRangeManager;

    BulletManager bulletManager;
    WaveManager waveManager;
    [SerializeField]
    private ParticleSystem feverParticle;

    WeaponShowManager weaponShowManager;


    public int playerWeapon = 0;
    public static int feverFlag = 0;

    //private int  kariban= 1;

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
        weaponShowManager = FindAnyObjectByType<WeaponShowManager>();
        WeaponReset();
        StartCoroutine(feverTime());
        //selectWeaponNumberの保証
        if(weaponShowManager.selectWeaponNumber != 1 ||
        weaponShowManager.selectWeaponNumber != 2 ||
        weaponShowManager.selectWeaponNumber != 3)
            weaponShowManager.selectWeaponNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WeaponReset()
    {
        //hitRangeManager.weaponState = weaponFirstState[(int)weaponShowManager.selectWeaponNumber - 1];
        hitRangeManager.weaponState = weaponFirstState[weaponShowManager.selectWeaponNumber - 1];
    }

    public static int feverCount;
    int feverMax;
    private IEnumerator feverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            //Debug.Log(feverFlag);
            feverMax = WaveManager.wave * 2 + 5;
            //Effectとの兼ね合いで定数に
            float feverDuration = 5;
            feverCount += 1;

            //2000はfeverが0になる制限時間
            if (feverCount >= 2000)
            {
                feverCount = 0;
                feverFlag = 0;
            }
            else
            {
                if (feverFlag >= feverMax)
                {
                    if (weaponShowManager.selectWeaponNumber == 1)
                    {
                        var state = hitRangeManager.weaponState;
                        float wavechecker_0 = 2 + WaveManager.wave * 0.5f;
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange * wavechecker_0,
                        FiringInterval: state.FiringInterval / wavechecker_0,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed * wavechecker_0
                        );
                        ParticleSystem feverInstance = Instantiate(feverParticle);
                        var feverMat = feverInstance.GetComponent<ParticleSystemRenderer>().material;
                        feverMat.renderQueue = 3000;
                        feverInstance.transform.position = new Vector3(0, 0, 0);
                        feverInstance.Play();
                        yield return new WaitForSeconds(feverDuration);
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange,
                        FiringInterval: state.FiringInterval,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed
                        );
                        feverFlag = feverDecrease(feverFlag);
                    }
                    if (weaponShowManager.selectWeaponNumber == 2)
                    {
                        var state = hitRangeManager.weaponState;
                        float wavechecker_1 = 2 * WaveManager.wave;
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange,
                        FiringInterval: state.FiringInterval / wavechecker_1,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed
                        );
                        ParticleSystem feverInstance = Instantiate(feverParticle);
                        var feverMat = feverInstance.GetComponent<ParticleSystemRenderer>().material;
                        feverMat.renderQueue = 3000;
                        feverInstance.transform.position = new Vector3(0, 0, 0);
                        feverInstance.Play();
                        yield return new WaitForSeconds(feverDuration);
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange,
                        FiringInterval: state.FiringInterval,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed
                        );
                        feverFlag = feverDecrease(feverFlag);
                    }
                    if (weaponShowManager.selectWeaponNumber == 3)
                    {
                        var state = hitRangeManager.weaponState;
                        float wavechecker_2 = 2 * WaveManager.wave;
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange * wavechecker_2,
                        FiringInterval: state.FiringInterval,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed
                        );
                        ParticleSystem feverInstance = Instantiate(feverParticle);
                        var feverMat = feverInstance.GetComponent<ParticleSystemRenderer>().material;
                        feverMat.renderQueue = 3000;
                        feverInstance.transform.position = new Vector3(0, 0, 0);
                        feverInstance.Play();
                        yield return new WaitForSeconds(feverDuration);
                        hitRangeManager.weaponState = new WeaponState(
                        HitRange: state.HitRange,
                        FiringInterval: state.FiringInterval,
                        Attack: state.Attack,
                        BulletSpeed: state.BulletSpeed
                        );
                        feverFlag = feverDecrease(feverFlag);
                    }
                }
            }
        }
    }
    private int feverDecrease(int feverFlag)
    {
        if (feverFlag > 20)
            feverFlag = 5;
        else if (feverFlag > 15)
            feverFlag = 4;
        else
            feverFlag = 2;

        return feverFlag;
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
