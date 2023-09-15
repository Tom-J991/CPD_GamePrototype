using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTextureChanger : MonoBehaviour
{
    public Material BaseMaterial;
    public Material MobileMaterial;
    // Start is called before the first frame update
    void Start()
    {
        Material matToLoad = null;
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                matToLoad = MobileMaterial;
                break;
            default:
                matToLoad = BaseMaterial;
                break;
        }
        transform.GetComponent<MeshRenderer>().material = matToLoad;
    }

    
}
