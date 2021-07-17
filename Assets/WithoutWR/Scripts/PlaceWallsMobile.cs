using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlaceWallsMobile : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private Image debugGO;
    [SerializeField]
    private PlaceWalls placeW;
    public void OnDrag(PointerEventData ped)
    {
        //Debug.Log(ped.position);
        //placeW.MoveWall(Input.mousePosition);
        //Debug.Log(Input.mousePosition);
        //placeW.MoveWall(Input.GetTouch(0).position);
        ////debugGO.rectTransform.anchoredPosition= Input.GetTouch(0).position;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        //Debug.Log(ped.position);
        //Debug.Log(Input.mousePosition);
        //OnDrag(ped);
        //// Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData ped)
    {
        //Debug.Log(ped.position);
        //Debug.Log(Input.mousePosition);
        ////placeW.PlaceWall();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
                placeW.MoveWall(touch.position);
            if (touch.phase == TouchPhase.Ended)
                placeW.PlaceWall(touch.position);
        }
    }
}
