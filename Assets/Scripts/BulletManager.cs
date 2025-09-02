using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
        StartCoroutine(BulletMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BulletMove()
    {
        float count = 1.0f;
        Vector3 bulletScale;
        Debug.Log("animation test");
        while (transform.localScale.magnitude >= 1.2f)
        {
            bulletScale = new Vector3(5 / count, 5 / count, 1);
            //transform.localScale -= new Vector3(0.1f, 0.1f, 0);
            transform.localScale = bulletScale;
            yield return new WaitForSeconds(0.01f);
            count += 0.2f;
        }

        Destroy(this.gameObject);
    }
}
