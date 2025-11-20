using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    Slider m_Slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume ;
    }

    private void OnEnable()
    {
        m_Slider.value = AudioListener.volume;
        m_Slider.onValueChanged.AddListener((sliderValue) => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        m_Slider.onValueChanged.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
