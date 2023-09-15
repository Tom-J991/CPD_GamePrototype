//Gyro manager
//by Jackson
//Last edited 14/9/23 11:44 pm
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    #region Instance
    private static GyroManager m_instance;
    public static GyroManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GyroManager>();
                if(m_instance == null)
                {
                    m_instance = new GameObject("Spawned GyroManager", typeof(GyroManager)).GetComponent<GyroManager>();
                }
            }
            return m_instance;
        }
        set
        {
            m_instance = value;
        }
    }
    #endregion

    [Header("Logic")]
    Gyroscope m_gyro;
    Quaternion m_rotation;
    bool m_gyroActive;

    public void EnableGyro()
    {
        if (m_gyroActive) return;

        if(SystemInfo.supportsGyroscope)
        {
            m_gyro = Input.gyro;
            m_gyro.enabled = true;
            m_gyroActive = m_gyro.enabled;
        }

    }
    void Update()
    {
        if(m_gyroActive)
            m_rotation = m_gyro.attitude;
    }
    public Quaternion GetGyroRotation()
    {
        return m_rotation;
    }
}
