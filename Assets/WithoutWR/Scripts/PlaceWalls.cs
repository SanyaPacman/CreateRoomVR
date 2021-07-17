using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceWalls : MonoBehaviour
{

    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject parent;


    private Camera cam;
    private GameObject[] pointsForPlace;
    private Quaternion localRotation;


    // Start is called before the first frame update
    void Start()
    {
        localRotation = new Quaternion();
        cam = Camera.main;
        parent = FindObjectOfType<Room>().gameObject;
    }
    public void RotateOnDegrees(float degree)
    {
        localRotation.eulerAngles += new Vector3(0, degree, 0);
        wall.transform.localRotation = localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        //для теста на пк
        MoveWall(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            PlaceWall(Input.mousePosition);
        }
    }
    private bool CheckTouchUIButtons(Vector2 touchOnScreen)
    {
        PointerEventData clickData = new PointerEventData(EventSystem.current);
        clickData.position = touchOnScreen;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(clickData, results);
        if (results.Count != 0)
            return true;
        else
            return false;
    }
    public void MoveWall(Vector2 touchOnScreen)
    {
        if (CheckTouchUIButtons(touchOnScreen))
            return;
        pointsForPlace = GameObject.FindGameObjectsWithTag("PlacePoint");
        if (cam == null)
        {
            Debug.Log("Camera not found");
            return;
        }
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(touchOnScreen);

        if (Physics.Raycast(ray, out hit))
        {
            //wall.transform.position = hit.point;
            //wall.transform.position = findNearestTarget(wall,pointsForPlace).transform.position;
            wall.transform.position = findNearestTarget(hit.point, pointsForPlace).transform.position;
        }
    }
    public void  PlaceWall()
    {
        //Instantiate(wall, wall.transform.position, wall.transform.rotation);
        var inst = Instantiate(wall, parent.transform);
        inst.transform.position = wall.transform.position;
        inst.transform.localRotation = localRotation;
    }

    public void PlaceWall(Vector2 touchOnScreen)
    {
        if (CheckTouchUIButtons(touchOnScreen))
            return;
        //Instantiate(wall, wall.transform.position, wall.transform.rotation);
        var inst = Instantiate(wall, parent.transform);
        inst.transform.position = wall.transform.position;
        inst.transform.localRotation = localRotation;
    }
    private GameObject findNearestTarget(Vector3 position, GameObject[] targets)
    {
        
        GameObject nearestGO = targets[0];
        float minimumDistance = Vector3.Distance(nearestGO.transform.position, position);
        for (int i = 1; i < targets.Length; i++)
        {
            float curDistance= Vector3.Distance(targets[i].transform.position, position);
            if (minimumDistance> curDistance)
            {
                minimumDistance = curDistance;
                nearestGO = targets[i];
            }
        }
        return nearestGO;
    }
    private GameObject findNearestTarget(GameObject go, GameObject[] targets)
    {

        GameObject nearestGO = targets[0];
        float minimumDistance = Vector3.Distance(nearestGO.transform.position, go.transform.position);
        for (int i = 1; i < targets.Length - 1; i++)
        {
            float curDistance = Vector3.Distance(targets[i].transform.position, go.transform.position);
            if (minimumDistance > curDistance)
            {
                minimumDistance = curDistance;
                nearestGO = targets[i];
            }
        }
        return nearestGO;
    }
}
