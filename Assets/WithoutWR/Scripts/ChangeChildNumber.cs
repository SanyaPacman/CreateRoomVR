using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChildNumber : MonoBehaviour
{
    public int minimumChildCount;
    public void CloneLastChild()
    {
        var clone= transform.GetChild(transform.childCount - 1);
        Instantiate(clone, clone.position, clone.rotation, transform);
    }
    public void DeleteLastChild()
    {
        if (transform.childCount<=0|| transform.childCount <=minimumChildCount)
        {
            return;
        }
        var clone = transform.GetChild(transform.childCount - 1);
        Destroy(clone.gameObject);
    }
}
