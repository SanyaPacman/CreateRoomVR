using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : MonoBehaviour
{
    [SerializeField]
    private ProceduralRoomGeneration generation;

    public InputField InputX;

    public InputField InputY;
    
    public InputField InputZ;
    
   public void TrySendDataToGenerator()
    {        
        float x, y, z;
        float.TryParse(InputX.text, out x);
        float.TryParse(InputY.text, out y);
        float.TryParse(InputZ.text, out z);
        generation.sizeX = x; 
        generation.sizeY = y;
        generation.sizeZ = z;
        generation.GenerateMesh();
    }
}
