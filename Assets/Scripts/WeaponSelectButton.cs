using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    public List<GameObject> weapons;
    public static int selectWeaponNumber;

    void Start()
    {
        selectWeaponNumber = 1;
        HideAllWeapons();
    }

    public static int WeaponNumber()
    {
        return selectWeaponNumber;
    }

    public void OnClickSelect(int index)
    {
        selectWeaponNumber = index;
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == index - 1);
        }
    }
    void HideAllWeapons()
    {
        foreach (var c in weapons)
        {
            c.SetActive(false);
        }

    }
}
