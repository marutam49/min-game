using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShowManager : MonoBehaviour
{
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public int selectWeaponNumber;
    void Awake()
    {
        Weapon1.SetActive(false);
        Weapon2.SetActive(false);
        Weapon3.SetActive(false);
        selectWeaponNumber = WeaponSelectButton.selectWeaponNumber;
        if (selectWeaponNumber == 1)
        {
            Weapon1.SetActive(true);
        }
        if (selectWeaponNumber == 2)
        {
            Weapon2.SetActive(true);
        } 
        if(selectWeaponNumber == 3)
        {
            Weapon3.SetActive(true);
        } 
    } 
}