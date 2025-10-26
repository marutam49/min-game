using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;

public class FeverGaugeController : MonoBehaviour
{
    [SerializeField] private GameObject _gauge;
    [SerializeField] private GameObject _graceGauge;

    WeaponManager weaponManager;
    float _feverFlag = WeaponManager.feverFlag;
    private float _feverFlag1;
    private float _waitingTime = 0.1f;
    void Awake()
    {
        _feverFlag1 = _gauge.GetComponent<RectTransform>().sizeDelta.x / _feverFlag;
    }

    public void feverGaugePlus()
    {
        float feverPlus = _feverFlag1;
        StartCoroutine(damageEm(feverPlus));
    }
    // Update is called once per frame
    IEnumerator damageEm(float feverPlus)
    {
        UnityEngine.Vector2 nowsafes = _gauge.GetComponent<RectTransform>().sizeDelta;
        nowsafes.x += feverPlus;
        _gauge.GetComponent<RectTransform>().sizeDelta = nowsafes;

        yield return new WaitForSeconds(_waitingTime);
        _graceGauge.GetComponent<RectTransform>().sizeDelta = nowsafes;
    }
}
