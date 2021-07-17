using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlaceItem : MonoBehaviour
    {
        private GameObject placebalePrefab;
        public GameObject PlacebalePrefab 
        { 
            get { return placebalePrefab; }
            set 
            {
                if (marker!=null)
                {
                    Destroy(marker);
                }
                placebalePrefab = value;
                marker= Instantiate(placebalePrefab);
                marker.layer = LayerMask.NameToLayer("Ignore Raycast");
                for (int i = 0; i < marker.transform.childCount; i++)
                {
                    marker.transform.GetChild(i).gameObject.layer= LayerMask.NameToLayer("Ignore Raycast");
                }
            } 
        }

        private Vector3 PlacePoint;
        private GameObject camGO;
        private GameObject marker;
        // Use this for initialization
        void Start()
        {
            camGO = Camera.main.gameObject;
        }
        private void Detect()
        {
            if (PlacePoint == null|| marker==null)
                return;
            RaycastHit hit;            
            Debug.DrawRay(camGO.transform.position, camGO.transform.forward);
            if (Physics.Raycast(camGO.transform.position, camGO.transform.forward, out hit, LayerMask.NameToLayer("Wall"))) 
            {
                PlacePoint = hit.point;
                marker.transform.position = PlacePoint;
            }
        }
        public void Place()
        {
            Instantiate(marker, marker.transform.position, marker.transform.rotation);
            PlacebalePrefab = null;
        }

        public void RotateMarker(float angle)
        {
            if (marker == null)
                return;
            marker.transform.Rotate(marker.transform.up, angle);
        }
        // Update is called once per frame
        void Update()
        {
            Detect();
            if (PlacePoint == null || placebalePrefab == null)
                return;
            Debug.DrawLine(camGO.transform.position, PlacePoint);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Place();                
            //}
        }
        

    }
}