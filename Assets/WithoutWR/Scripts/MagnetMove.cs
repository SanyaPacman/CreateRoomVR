
using System.Collections;
using UnityEngine;


public class MagnetMove : MonoBehaviour
{
        [SerializeField]
        private float stepX;
        [SerializeField]
        private float stepZ;

        public void SetPosition(Vector3 pos)
        {
            transform.position = new Vector3(RoundByStep(pos.x,stepX), transform.position.y, RoundByStep(pos.z, stepZ));
        }
    private float RoundByStep(float number, float step)
    {
        return Mathf.Round(number / step) * step;
        
        //Debug.Log(string.Format("number {0}", number));
        //float remains = number % step;
        
        //Debug.Log(string.Format( "remains {0}",remains));
        //float fullPart =  number / step - remains;
        //Debug.Log(string.Format("fullpart {0}",fullPart));
        //if (remains > step / 2)
        //    return (fullPart + 1) * step;
        //else
        //    return (fullPart) * step;
    }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
 }