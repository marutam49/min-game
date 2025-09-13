using Unity.Mathematics;
using UnityEngine;

//弾の生成
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireAnimation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -8;
        Instantiate(bullet, mousePos, Quaternion.identity);
    }
}
