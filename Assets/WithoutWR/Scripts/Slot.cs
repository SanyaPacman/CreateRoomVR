using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private GameObject prefab;
        private PlaceItem player;
            

        public void SetPrefab()
        {
            player.PlacebalePrefab = prefab;
        }
        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlaceItem>();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}