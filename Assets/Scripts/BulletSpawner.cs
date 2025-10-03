using Unity.Mathematics;
using UnityEngine;

//弾の生成
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    [SerializeField] AudioClip shooting1;

    [SerializeField] AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(shooting1);
        //Debug.Log("a");
    }
}
