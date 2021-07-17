using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWallButton : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    ProceduralWallGeneration wallGeneration;
    public void SendData()
    {
        float res;
        string str=  inputField.text.Replace('.', ',');
        if (float.TryParse(str, out res))
        {
            wallGeneration.SetHeigth(res);
        }

    }
}
