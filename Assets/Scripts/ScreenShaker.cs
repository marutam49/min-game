using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public float duration = 0.5f;      // 振動時間
    public float magnitude = 0.1f;     // 振動の強さ

    Vector3 originalPos;
    float elapsed = 0f;
    bool isShaking = false;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            if (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = originalPos + new Vector3(x, y, 0);
                elapsed += Time.deltaTime;
            }
            else
            {
                isShaking = false;
                elapsed = 0f;
                transform.localPosition = originalPos;
            }
        }
    }

    public void Shake(float shakeDuration = -1f, float shakeMagnitude = -1f)
    {
        if (shakeDuration > 0) duration = shakeDuration;
        if (shakeMagnitude > 0) magnitude = shakeMagnitude;
        originalPos = transform.localPosition;
        elapsed = 0f;
        isShaking = true;
    }
}