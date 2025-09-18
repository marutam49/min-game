using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    [SerializeField] Slider DashGage;

    public float maxDashPoint = 20;

    float dashPoint;
    float currentVelocity = 0;

    void Start()
    {
        dashPoint = maxDashPoint;
        DashGage.maxValue = maxDashPoint;
        DashGage.value = 0;
    }


    void Update()
    {
        int exp = LevelManager.exp;
        float currentDashPT = Mathf.SmoothDamp(DashGage.value, dashPoint, ref currentVelocity, 10 * Time.deltaTime);
        DashGage.value = currentDashPT;
        DashGage.value = exp;
    }
}