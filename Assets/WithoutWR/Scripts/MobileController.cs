using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour
{
    [SerializeField]
    private MobileJoystick joyRotate;
    [SerializeField]
    private MobileJoystick joyMove;
    [SerializeField]

    public float HorizontalRotate()
    {
        return joyRotate.Horizontal();
    }
    public float VerticalRotate()
    {
        return joyRotate.Vertical();
    }

    public float HorizontalMove()
    {
        return joyMove.Horizontal();
    }
    public float VerticalMove()
    {        
            return joyMove.Vertical();       
    }
}
