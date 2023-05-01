using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE
{
    public class TriggerShowAtDistance : MonoBehaviour
    {
        public float distanceToTrigger;

        // Update is called once per frame
        void Update()
        {
            //if( GameManager.Instance.GetPlayer() != null)
            //{
            //    Vector3 playerPosition = GameManager.Instance.GetPlayer().gameObject.transform.position;

            //    float distance = Vector3.Distance(transform.position, playerPosition);

            //    if(distance >= distanceToTrigger)
            //    {
            //        if( GetComponent<Animator>() != null
            //            ) 
            //        {
            //            GetComponent<Animator>().SetTrigger("Show");
            //        }
            //    }

            //}
        }
    }
}
