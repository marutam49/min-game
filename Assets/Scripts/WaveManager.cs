using UnityEngine;

//wave切り替えの管理
public class WaveManager : MonoBehaviour
{
    [SerializeField] private MoleSpawner moleSpawner;

    public static int wave = 1;

    public int enemyBeatNumber = 0;
    public int waveEnemyBeatQuota = 30;

    void Start()
    {

    }


    void Update()
    {

    }


    public void WaveAdd()
    {
        wave += 1;
        moleSpawner.WaveUpdate();
        enemyBeatNumber = 0;
    }

}
