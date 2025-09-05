using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] HitRangeManager hitRangeManager;

    public int playerWeapon = 0;

    //{hitRange, firingInterval, attack, bulletSpeed}(bulletSpeedは未実装)
    float[,] weaponFirstState = new float[1, 4]{
        {0.3f, 0.2f, 1, 0.4f}
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponReset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void weaponReset()
    {
        hitRangeManager.hitRange = weaponFirstState[playerWeapon, 0];
        hitRangeManager.firirngInterval = weaponFirstState[playerWeapon, 1];
        hitRangeManager.attack = (int)weaponFirstState[playerWeapon, 2];
    }
}
