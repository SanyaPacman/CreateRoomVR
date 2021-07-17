using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class ChangeVRMode : MonoBehaviour
{
    IEnumerator SwitchToVr()
    {
        XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        XRSettings.enabled = true;
        
    }
    IEnumerator SwitchToNormal()
    {
        //XRSettings.LoadDeviceByName("None");
        yield return null;
        XRSettings.enabled = false;
        
    }
    IEnumerator WaitForLandscape()
    {
        while (Screen.orientation != ScreenOrientation.LandscapeLeft)
        {
            yield return null;
        }
        //XRSettings.LoadDeviceByName("cardboard");
    }
    public void LoadWithVR(int NumberScene)
    {
        //bool oldVal = XRSettings.enabled;
        //XRSettings.enabled = true;
        //if (!oldVal)
        //{
        //StartCoroutine(SwitchToVr());
        SceneManager.LoadScene(NumberScene);
        //}         
    }
    public void SetNormal()
    {
        StartCoroutine(SwitchToNormal()); 
    }
    public void Start()
    {
        SetNormal();
    }
}
