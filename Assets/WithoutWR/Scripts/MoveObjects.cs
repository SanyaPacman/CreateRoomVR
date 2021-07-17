using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [SerializeField]
    private float cameraMoveSensetive=0.5f;
    [SerializeField]
    private float cameraScaleSensetive = 0.05f;
    private enum inputMode
        {
         moveObject,moveCamera, scale,
        }
    float yCam;
    float camScale;
    // Start is called before the first frame update
    void Start()
    {
        yCam = Camera.main.transform.position.y;
        camScale = Camera.main.orthographicSize;
    }
    
    private void Logic(inputMode inputMode)
    {
        switch (inputMode)
        {
            case inputMode.moveObject:
                break;
            case inputMode.moveCamera:
                break;
            case inputMode.scale:
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    
    Vector3 touchToWorld;
    Vector3 startWorldPosition = new Vector3();
    Vector2 startUiPosition = new Vector2(0, 0);
    MagnetMove movementobj = null;
    inputMode mode = default;
    Vector2 endUiPosition = new Vector2(0, 0);
    void Update()
    {
        Camera cam = Camera.main;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {                
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {                    
                    movementobj = hit.transform.GetComponent<MagnetMove>();
                    Debug.Log(movementobj);
                    if (movementobj != null)
                    {
                        mode = inputMode.moveObject;
                        touchToWorld = cam.transform.InverseTransformPoint(0, 0, 0);
                    }   
                    else
                    {
                        
                        mode = inputMode.moveCamera;
                        startWorldPosition = cam.transform.position;
                        startUiPosition = touch.position;
                    }
                }
                else
                {
                    mode = inputMode.moveCamera;
                    startWorldPosition = cam.transform.position;
                    startUiPosition = touch.position;
                }
            }
            Debug.Log(mode.ToString());
            Debug.Log(startWorldPosition);
            if (touch.phase==TouchPhase.Moved)
            {                
                switch (mode)
                {
                    case inputMode.moveObject:                        
                        var cameraTransform = cam.transform.InverseTransformPoint(0, 0, 0);
                        var saveY = movementobj.transform.position.y;
                        touchToWorld = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y));
                        movementobj.SetPosition(new Vector3(touchToWorld.x, saveY, touchToWorld.z));
                        break;
                    case inputMode.moveCamera:
                        touchToWorld = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y));
                        Vector3 FromStartToCurrentTouch = startUiPosition- touch.position;
                        cam.transform.position = startWorldPosition + new Vector3(FromStartToCurrentTouch.x * cameraMoveSensetive * 0.05f, yCam, FromStartToCurrentTouch.y * cameraMoveSensetive * 0.05f);
                        break;
                }
            }           
        }
        if (Input.touchCount>=2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            mode = inputMode.scale;
            if (touch2.phase==TouchPhase.Began)
            {
                startUiPosition = touch1.position;
                endUiPosition = touch2.position;
            }
            if (touch2.phase==TouchPhase.Moved)
            {
                float differenceDistances= Vector2.Distance(startUiPosition, endUiPosition) - Vector2.Distance(touch1.position, touch2.position);
                cam.orthographicSize = camScale + differenceDistances* cameraScaleSensetive;
                if (cam.orthographicSize<=0.1f)
                {
                    cam.orthographicSize = 0.1f;
                }
            }
            if (touch2.phase==TouchPhase.Ended)
            {
                camScale = cam.orthographicSize;
            }
            
        }
    }
}