using System.Collections;
using UnityEngine;

//クリック範囲の表示、クリック時の各種パラメータの管理
public class HitRangeManager : MonoBehaviour
{
    public bool doHitDecision = false;
    public float hitRange = 1.0f;
    public float firirngInterval = 1.0f;
    
    void Start()
    {

    }


    float durationAfterClick = 0;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -9;
        transform.position = mousePos;

        transform.localScale = new Vector3(hitRange, hitRange, hitRange);

        //クリック時に判定を出せるかの判定
        if (Input.GetMouseButton(0))
        {
            durationAfterClick += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) || durationAfterClick >= firirngInterval)
            {
                durationAfterClick = 0;
                StartCoroutine(Fire());
            }
        }
    }


    //判定を出す時の処理
    IEnumerator Fire()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        doHitDecision = true;

        yield return new WaitForSeconds(0.05f);

        gameObject.GetComponent<Renderer>().material.color = Color.white;
        doHitDecision = false;
    }
}
