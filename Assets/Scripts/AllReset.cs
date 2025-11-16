using UnityEngine;

public class AllReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelManager.exp = 0;
        LevelManager.level = 0;
        //PauseManager.isPaused = false;
        WaveManager.wave = 1;
        WeaponManager.feverFlag = 0;
        WeaponManager.feverCount =0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
