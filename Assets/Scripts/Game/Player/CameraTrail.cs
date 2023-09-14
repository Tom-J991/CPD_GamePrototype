//Camera Trail
//by Jackson
//Last Edited 14/9/23 12:17 pm
using Unity.VisualScripting;
using UnityEngine;

public class CameraTrail : MonoBehaviour
{
    [SerializeField]
    Transform m_targetObject;
    Rigidbody m_targetRigidbody;
    [SerializeField]
    Vector2 m_followDistances;
    [SerializeField]
    float m_followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_targetRigidbody = m_targetObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 flatVelocity = new Vector2(m_targetRigidbody.velocity.x, m_targetRigidbody.velocity.z).normalized;

        transform.position = Vector3.Lerp(transform.position, new Vector3((flatVelocity * -m_followDistances.x).x + m_targetObject.position.x, m_targetObject.position.y + m_followDistances.y, (flatVelocity * -m_followDistances.y).y + m_targetObject.position.z), m_followSpeed );
        transform.LookAt(m_targetObject.position);
    }
}
