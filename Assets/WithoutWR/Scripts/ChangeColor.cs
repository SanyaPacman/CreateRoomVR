using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
  public void Red()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void Black()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue; 
    }

}
