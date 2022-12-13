using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowCamera : MonoBehaviour
{
    public GameObject FollowObj;
    public float smoothSpeed = 0.125f; //FollowSensitive

    [Header("Follow Dynamic Smooth Angle")]
    public Vector3 offset;

    bool delay = true;

    public static bool Finish = false;

    [SerializeField]
    public GameObject FinishPos;

    private void Start()
    {
        Finish = false;
    }

    void FixedUpdate()
    {

        if (Finish == false)
        {

            LookAtPlayer();


            if (smoothSpeed != 0.125f)
            {
                smoothSpeed = Mathf.Clamp(smoothSpeed + 0.001f * Time.fixedDeltaTime, 0, 0.125f);
            }

            if (FollowObj != null)
            {
                //LookAtPlayer();
                float dist = Vector3.Distance(FollowObj.transform.position, transform.position);

                if (dist > 0f)
                {
                    Vector3 desiredPoisiton = FollowObj.transform.position + offset;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPoisiton, smoothSpeed);
                    transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
                }

            }
        }

        else
        {


            FinishLookAtPlayer();


            this.gameObject.transform.DOMove(FinishPos.transform.position, 4f);

            if (smoothSpeed != 1f)
            {
                smoothSpeed = Mathf.Clamp(smoothSpeed + 1 * Time.fixedDeltaTime, 0.1f, 0.2f);

            }

        }

    }

    void LookAtPlayer()
    {
        var ey = new Vector3(FollowObj.transform.position.x, FollowObj.transform.position.y, FollowObj.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(ey - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 0.075f);
    }

    void FinishLookAtPlayer()
    {
        var ey = new Vector3(FollowObj.transform.position.x, this.gameObject.transform.position.y, FollowObj.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(ey - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 3f);
    }

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(7f);
    //    delay = false;
    //}
}
