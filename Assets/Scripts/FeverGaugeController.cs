using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverGaugeController : MonoBehaviour
{

    [SerializeField] Slider FeverGauge;

    WeaponManager weaponManager;

    public float maxfeverPoint = 5;

    float feverPoint;
    float currentVelocity = 0;

    void Start()
    {
        feverPoint = maxfeverPoint;
        FeverGauge.maxValue = maxfeverPoint;
        FeverGauge.value = 0;
    }


    void Update()
    {
        int  count = WeaponManager.feverFlag;
        float currentDashPT = Mathf.SmoothDamp(FeverGauge.value, feverPoint, ref currentVelocity, 10 * Time.deltaTime);
        FeverGauge.value = currentDashPT;
        FeverGauge.value = count ;
    }
}