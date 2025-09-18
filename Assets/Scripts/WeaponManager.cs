using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] HitRangeManager hitRangeManager;

    BulletManager bulletManager;

    public int playerWeapon = 0;

    //{hitRange, firingInterval, attack, bulletSpeed}(bulletSpeedは未実装)
    WeaponState[] weaponFirstState = new WeaponState[]{
        new WeaponState(
            HitRange: 0.3f,
            FiringInterval: 0.2f,
            Attack: 10,
            BulletSpeed: 0.1f
        )
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletManager = FindAnyObjectByType<BulletManager>();
        WeaponReset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WeaponReset()
    {
        hitRangeManager.weaponState = weaponFirstState[0];
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