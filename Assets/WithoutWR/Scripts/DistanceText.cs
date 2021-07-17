using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceText : MonoBehaviour
{
    public Text distanceText;
    public Text distancePlaceHolder;
    public InputField IF;
    //public Text text;
    private Transform grandParent;
    private Transform parent;
    private Transform nextparent;
    private float startedY;
    // Start is called before the first frame update
    void Start()
    {
        grandParent = transform.parent.parent;
        parent = transform.parent;
        startedY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //нахождение следующего дочернего объекта в grandparente-e
        int indexParent = parent.GetSiblingIndex();
        if (indexParent+1>grandParent.childCount-1)
        {
            nextparent = grandParent.GetChild(0).transform;
        }
        else
            nextparent = grandParent.GetChild(indexParent + 1).transform;

        Vector3 nextCurrVector = nextparent.position - parent.position;
        nextCurrVector.y = 0;
        //расположение канваса(текста) между точками
        transform.position = parent.position + nextCurrVector / 2;
        transform.position=new Vector3(transform.position.x, startedY, transform.position.z);
        distanceText.text = string.Format("{0:f2}", nextCurrVector.magnitude);
        distancePlaceHolder.text = string.Format("{0:f2}", nextCurrVector.magnitude);
    }

    public void ChangeDistance()
    {
        float res;
        string str = distanceText.text.Replace('.', ',');
        if (float.TryParse(str, out res))
        {
            nextparent.position = parent.position+ (nextparent.position - parent.position).normalized * res;                     
            IF.text="";
        }
        
    }
    IEnumerator Deselect()
    {
        if (!distancePlaceHolder.enabled)
        {
            distancePlaceHolder.enabled = true;
            yield return null;
        }        
        //yield return null;

    }
}
