using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private static Room room;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SimpleLoadScene(int index)
    {
        SceneManager.LoadScene(index);
        if (room!=null)        
            Destroy(room);
    }
    public void LoadSceneWithRoom( int index)
    {
        room = FindObjectOfType<Room>();
        if (room==null)
        {
            return; 
        }
        room.transform.parent = null;
        DontDestroyOnLoad(room.gameObject);
        SceneManager.LoadScene(index);               
        //Room[] rooms = FindObjectsOfType<Room>();
        //foreach (var curentRoom in rooms)
        //{
        //    if (curentRoom!=room)            
        //        Destroy(curentRoom.gameObject);            
        //}
        //Debug.Log(rooms.Length+"number rooms");
        
    }
}
