using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeTextPerPlatform : MonoBehaviour
{
    [SerializeField]
    string DesktopText;
    [SerializeField]
    string MobileText;
    [SerializeField]
    string ConsoleText;

    TextMeshProUGUI m_textBox;

    void Awake()
    {
        m_textBox = GetComponent<TextMeshProUGUI>();
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Desktop:
                m_textBox.text = DesktopText;
                break;
            case DeviceType.Handheld:
                m_textBox.text = MobileText;
                break;
            case DeviceType.Console:
                m_textBox.text = ConsoleText;
                break;
        }
    }
}
