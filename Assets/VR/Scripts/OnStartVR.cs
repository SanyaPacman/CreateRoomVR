using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnStartVR : MonoBehaviour
{
    IEnumerator SwitchToVr()
    {
        XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        XRSettings.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchToVr());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
