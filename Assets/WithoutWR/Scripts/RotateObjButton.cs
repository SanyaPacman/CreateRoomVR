using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class RotateObjButton : MonoBehaviour, IPointerDownHandler   
    {
        [SerializeField]
        private PlaceItem PI;
        public void OnPointerDown(PointerEventData eventData)
        {
            PI.RotateMarker(5);
        }
    }
}