using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.XR;

public class OnStartNormal : MonoBehaviour
{
    IEnumerator SwitchToNormal()
    {
        XRSettings.LoadDeviceByName("None");
        yield return null;
        XRSettings.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchToNormal());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
