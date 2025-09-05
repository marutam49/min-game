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
            hitRangeManager.hitRange += 0.01f;
            hitRangeManager.firirngInterval -= 0.01f;
            hitRangeManager.attack += 1;
        }
    }
}
