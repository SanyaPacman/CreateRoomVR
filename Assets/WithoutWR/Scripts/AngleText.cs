using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleText : MonoBehaviour
{    
    public Text angleText;
    //public Text text;
    Transform grandParent;
    Transform parent;
    Transform nextparent;
    Transform prevparent;
    // Start is called before the first frame update
    void Start()
    {
        grandParent = transform.parent.parent;
        parent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        int indexParent = parent.GetSiblingIndex();
        //нахождение следующего дочернего объекта в grandparent-е
        if (indexParent + 1 > grandParent.childCount - 1)
        {
            nextparent = grandParent.GetChild(0).transform;
        }
        else
            nextparent = grandParent.GetChild(indexParent + 1).transform;

        //нахождение предыдущего дочернего объекта в grandparent-е
        if (indexParent == 0)
        {
            prevparent = grandParent.GetChild(grandParent.childCount - 1).transform;
        }
        else
            prevparent = grandParent.GetChild(indexParent - 1).transform;


        //вектора для расчета угла
        Vector3 nextCurrVector = nextparent.position - parent.position;
        nextCurrVector.y = 0;
        Vector3 prevCurrVector = prevparent.position - parent.position;
        prevCurrVector.y = 0;

        angleText.text = string.Format("{0:f2} *", Vector3.Angle(prevCurrVector, nextCurrVector));

    }
}
