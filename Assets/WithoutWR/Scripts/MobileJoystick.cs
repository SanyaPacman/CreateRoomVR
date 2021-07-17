using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
   
    private Image joystickBG;    
    private Image joystick;    
    private Vector2 inputVector;
    public void OnDrag(PointerEventData ped)
    {
        Debug.Log("OnDrag");
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);//получение координат позиции касания на джостик
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);//установка точных координат из касания
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;//граница малого джостика

            joystick.rectTransform.anchoredPosition = new Vector2(
                inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2),
                inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
         Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
         Debug.Log("OnPointerUp");
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }

    // Start is called before the first frame update
    void Start()
    {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }



  
}
