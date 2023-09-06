//Update Slider Inputfield script
//by Jackson
//Last Edited 6/9/2023 4:27 pm

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Slider))]
public class UpdateSliderInputField : MonoBehaviour
{
    Slider m_slider;
    TMP_InputField m_inputfield;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
        m_inputfield = GetComponentInChildren<TMP_InputField>();
        UpdateInputField();
    }
    public void UpdateInputField()
    {
        m_inputfield.text = Mathf.RoundToInt(m_slider.value * 100).ToString();
    }
    public void UpdateSlider()
    {
        m_slider.value = Convert.ToInt32(m_inputfield.text) / 100.0f;
    }
}
