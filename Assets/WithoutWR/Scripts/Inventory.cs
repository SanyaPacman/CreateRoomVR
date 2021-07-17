using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private Canvas canvas;
        // Use this for initialization
        void Start()
        {
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                canvas.enabled = !canvas.enabled;
            }
        }
    }
}