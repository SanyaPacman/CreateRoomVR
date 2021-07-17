using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMunu : MonoBehaviour
{    
    public GameObject childEnabled;

    public void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        childEnabled.SetActive(true);
    }
}
