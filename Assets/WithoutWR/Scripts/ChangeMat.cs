using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private string targetTag;
    private GameObject[] targets;

    private int currentMat = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwapMaterial()
    {
        targets = GameObject.FindGameObjectsWithTag(targetTag);        
        
        if (material==null|| targets==null||targets.Length==0)
        {
            return;
        }
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<MeshRenderer>().material = material;
        }
        
        //target.GetComponent<MeshRenderer>().material = materials[currentMat];
    }

}
