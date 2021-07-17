using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CreateList : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ListObjects;
    public GameObject panel;
    public GameObject slotPrefab;
    void Start()
    {
        
    }

    public void GeneratePanel()
    {
        if (panel == null || ListObjects == null || slotPrefab == null)
            return;
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            Destroy(panel.transform.GetChild(i));
        }
        for (int i = 0; i < ListObjects.Length; i++)
        {
            GameObject newItem = Instantiate(slotPrefab, panel.transform);
            //newItem.GetComponent<Slot>().Prefab = ListObjects[i];
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
