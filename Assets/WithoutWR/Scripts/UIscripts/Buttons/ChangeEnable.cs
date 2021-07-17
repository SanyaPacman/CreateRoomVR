using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnable : MonoBehaviour
{
    public GameObject target;
    public void Change()
    {
        if (target != null)
            target.SetActive(!target.activeSelf);
    }
}
