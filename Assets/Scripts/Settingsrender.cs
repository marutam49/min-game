using UnityEngine;

public class Settingsrender : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 設定を最前面に配置
        Canvas canvas = GetComponent<Canvas>();
        if(canvas == null)
            canvas = gameObject.AddComponent<Canvas>();

        canvas.overrideSorting = true;
        canvas.sortingOrder = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
