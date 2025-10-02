using UnityEngine;
using TMPro;

public class waveTextManager : MonoBehaviour
{
    public static waveTextManager Instance;
    public TextMeshProUGUI titleText;
    public string displayMessage = "WAVE:1";

    public Vector3 startPosition;
    public Vector3 endPosition;
    public float startSize = 120f;
    public float endSize = 40f;
    public float duration = 10f; //移動時間

    public float elapsed = 0f;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleText.text = displayMessage;
        titleText.fontSize = startSize;
        titleText.rectTransform.anchoredPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            //位置とフォントサイズの補完
            titleText.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
            titleText.fontSize = Mathf.Lerp(startSize, endSize, t);
        }
    }

    public static void changeText(string newText)
    {
        if (Instance != null)
        {
            Instance.titleText.text = newText;
            Instance.elapsed = 0f;
        }
    }
}
