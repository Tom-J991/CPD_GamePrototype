// mobile camera controller
// by Jackson
// Last edited 21/9/23 4:11PM 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCameraController : MonoBehaviour
{
    float angle = 0f;
    [SerializeField]
    float m_rotationSpeed = 10f;
    [SerializeField]
    Vector2 m_distance = Vector2.zero;
    [SerializeField]
    Transform m_target;
    // Start is called before the first frame update
    void Start()
    {
        if(m_target == null)
        {
            Debug.LogError("No target set for this camera.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(angle > 2 * Mathf.PI)
        {
            angle = 0;
        }
        if(angle < 0)
        {
            angle = 2 * Mathf.PI;
        }
        transform.position = new Vector3(m_target.position.x + Mathf.Sin(angle) * m_distance.x, m_target.position.y + m_distance.y, m_target.position.z + Mathf.Cos(angle) * -m_distance.x);
        transform.LookAt(m_target);
    }

    public void ChangeAngle(int dir)
    {
        angle += m_rotationSpeed * dir * Time.deltaTime;
    }
}
