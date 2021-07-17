using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    static Room currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        if (currentRoom == null)
            currentRoom = this;
        if (currentRoom!=this)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
