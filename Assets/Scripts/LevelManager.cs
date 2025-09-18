using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] HitRangeManager hitRangeManager;

    public static int exp = 0;

    public static int level = 1;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (exp >= 20)
        {
            level += 1;
            exp -= 20;
            var state = hitRangeManager.weaponState;
            hitRangeManager.weaponState = new WeaponState(
                HitRange: state.HitRange + 0.01f,
                FiringInterval: state.FiringInterval - 0.01f,
                Attack: state.Attack + 1,
                BulletSpeed: state.BulletSpeed
            );
        }
    }
}
  