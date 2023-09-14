using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_rotAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_rotAmount.x, m_rotAmount.y, m_rotAmount.z, Space.Self);
    }
}
