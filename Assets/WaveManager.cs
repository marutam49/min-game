using UnityEngine;

//wave切り替えの管理
public class WaveManager : MonoBehaviour
{
    [SerializeField] private MoleSpawner moleSpawner;

    public static int wave = 1;

    public static int enemyBeatNumber = 0;
    int waveEnemyBeatQuota = 10;

    void Start()
    {

    }


    void Update()
    {
        if (enemyBeatNumber >= waveEnemyBeatQuota)
        {
            wave += 1;
            moleSpawner.WaveUpdate();
            enemyBeatNumber = 0;
        }
    }

}
