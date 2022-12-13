using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowJoint : MonoBehaviour
{
    [SerializeField] float smooth;
    public Transform Target;
    [SerializeField] float dist;

    void FixedUpdate()
    {

            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);

        transform.GetChild(0).position =
               Vector3.Slerp(new Vector3(
               Target.position.x, Target.position.y, Target.position.z),
               transform.GetChild(0).position, smooth);

        transform.GetChild(0).transform.eulerAngles = Target.transform.eulerAngles;

        //if (transform.GetChild(0).CompareTag("Ready"))
        //{

        //    var eyy = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, -3.1f);

        //    transform.GetChild(0).transform.position = Vector3.Slerp(eyy, transform.GetChild(0).position, 0.5f);
        //}


        for (int i = 1; i < transform.childCount; i++)
        {
            var distance = Vector3.Distance(transform.GetChild(i).position,
                transform.GetChild(i - 1).transform.GetChild(0).position);

            if (distance > dist)
            {

                transform.GetChild(i).position =
                    Vector3.Slerp(new Vector3(
                    transform.GetChild(i - 1).GetChild(0).position.x,
                    transform.GetChild(i - 1).GetChild(0).position.y,
                    transform.GetChild(i - 1).GetChild(0).position.z),
                    transform.GetChild(i).position, smooth);

                transform.GetChild(i).LookAt(transform.GetChild(i - 1));

                //if (transform.GetChild(i).CompareTag("Ready"))
                //{
                //    var ey = new Vector3(transform.GetChild(i).transform.position.x, transform.GetChild(i).transform.position.y, -3.1f);

                //    transform.GetChild(i).transform.position = Vector3.Slerp(ey, transform.GetChild(i).position, 0.5f);

                //}
            }

        }
    }
}
