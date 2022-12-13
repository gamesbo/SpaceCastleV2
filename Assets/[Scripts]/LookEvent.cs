using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEvent : MonoBehaviour
{
    public Transform LookTarget, LookHome;
    public Transform LookAtObj;
    public float RotateSpeed;
    [SerializeField] bool yAxisExcept;
    [SerializeField] ParticleSystem Fire;
    [SerializeField] Animator anim;
    [SerializeField] bool Dontlook;

    private void Start()
    {
        LookAtObj = LookTarget;
    }

    void FixedUpdate()
    {
        if (!Dontlook)
        {
            if (LookAtObj != null)
            {
                Vector3 lTargetDir = LookAtObj.position - transform.position;
                if (yAxisExcept)
                    lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.fixedDeltaTime * RotateSpeed);
            }

            if ((LookAtObj != null && LookAtObj.CompareTag("Dead"))  || (LookAtObj != null && !LookAtObj.gameObject.activeSelf))
            {
                LookAtObj = null;
            }
        }
    }

    public void LookToTarget()
    {
        LookAtObj = LookTarget;
    }

    public void LetsBurnIt()
    {
        Fire.loop = true;
        Fire.Play();
        anim.SetBool("Fire", true);
    }

    public void BurnStop()
    {
        Fire.loop = false;

    }

    public void LookToHome()
    {
        LookAtObj = LookHome;
        anim.SetBool("Fire", false);
    }
}
