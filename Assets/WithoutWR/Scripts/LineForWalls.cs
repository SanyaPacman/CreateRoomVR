using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineForWalls : MonoBehaviour
{
    LineRenderer lRend;
    private void Start()
    {
        lRend = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        lRend.positionCount = transform.childCount+1;

        for (int i = 0; i < transform.childCount; i++)
        {
            lRend.SetPosition(i,transform.GetChild(i).transform.position);
        }
        lRend.SetPosition(lRend.positionCount-1,lRend.GetPosition(0));
    }
}
