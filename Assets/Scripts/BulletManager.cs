using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//弾のアニメーションの制御
public class BulletManager : MonoBehaviour
{
    public float distanceFromCamera = 1.0f;

    //public float bulletSpeed = 0.2f;

    HitRangeManager hitRangeManager;

    WeaponManager weaponManager;

    void Start()
    {
        hitRangeManager = FindAnyObjectByType<HitRangeManager>();
        weaponManager = FindAnyObjectByType<WeaponManager>();
        //transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
        StartCoroutine(BulletMove());
    }


    void Update()
    {

    }

    IEnumerator BulletMove()
    {
        while (distanceFromCamera <= 15.0f)
        {
            transform.localScale = new Vector3(5 * hitRangeManager.weaponState.HitRange / distanceFromCamera, 5 * hitRangeManager.weaponState.HitRange / distanceFromCamera, 1);
            yield return new WaitForSeconds(0.01f);
            distanceFromCamera += hitRangeManager.weaponState.BulletSpeed;
        }

        Destroy(this.gameObject);
    }
}
