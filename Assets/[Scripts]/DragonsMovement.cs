using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using DG.Tweening;
using Dreamteck;

public interface IEntityDragon
{
    void HealthDamage(int damage);
    void Dead();
    void MoneyCalculate(int fee);

}

public class DragonsMovement : MonoBehaviour, IEntityDragon
{
    #region InterfaceImplement

    IEntityDragon interfaceEntity;

    public void HealthDamage(int damage)
    {
        HealthValue -= damage;
        DragonMesh.DOShakeScale(0.1f, 0.05f, 3).OnComplete(() => DragonMesh.DOScale(Vector3.one, 0.1f));


        if (HealthValue <= 0)
        {
            Dead();
        }
        
    }

    public void Dead()
    {
        if (!oneshotDead)
        {
            Fire.loop = false;
            DragonSpline.enabled = false;
            rb.isKinematic = false;
            rb.AddRelativeForce(Vector3.back * 100f);
            DragonMesh.DOScale(Vector3.zero, 4f);
            rb.AddRelativeTorque(Vector3.back * 20f);
            TargetPoint.tag = "Dead";
            tag = "Dead";
            TargetPoint.gameObject.SetActive(false);

            connectionEconomy.CalculateMoney(DragonHitFee);


            transform.parent = GarbageCollector;
            transform.DOScale(Vector3.zero, 1f).SetDelay(2f).OnComplete(() => this.gameObject.SetActive(false));
            transform.DOMoveY(-2, 1).SetDelay(2f);
            if (DeadParticle != null)
            {
                DeadParticle.Play();
                DeadParticle.transform.parent = null;
            }
            StartCoroutine(delay());

            oneshotDead = true;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);

    }

    public void MoneyCalculate(int Fee)
    {
        connectionEconomy.CalculateMoney(Fee);
        Haptic.MediumTaptic();
    }

    #endregion


    GameObject Economy;
    Economy.InGameEconomy connectionEconomy;
    [SerializeField] Transform LookBase, LookHome;
    [SerializeField] Transform DragonMesh;
    [SerializeField] Dreamteck.Splines.SplineFollower DragonSpline;
    [SerializeField] Transform GarbageCollector;
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem Fire;
    [SerializeField] GameObject TargetPoint;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem DeadParticle;
    [SerializeField] GameObject Dragon2AIs;
    bool oneshotDead;
    public int HealthValue = 100;
    public int DamageValue;
    int DragonHitFee = 5;
    float SpeedInside;
    public bool DontStop;


    public void StopFollow()
    {
        if (!DontStop)
        {
            anim.enabled = false;
            DragonSpline.followSpeed = 0;
            StartCoroutine(AgainMove());
        }
    }

    IEnumerator AgainMove()
    {
        yield return new WaitForSeconds(20f);
        anim.enabled = true;
        DragonSpline.followSpeed = SpeedInside;
    }

    private void Start()
    {
        Economy = GameObject.FindWithTag("Economy");
        connectionEconomy = GameObject.FindWithTag("Economy").GetComponent<Economy.InGameEconomy>();
        interfaceEntity = GetComponent<IEntityDragon>();
        SpeedInside = DragonSpline.followSpeed;
    }

    public void Dragon2AIGenerate()
    {
        var ey = Instantiate(Dragon2AIs, Dragon2AIs.transform.position, Dragon2AIs.transform.rotation, Dragon2AIs.transform.parent);
        ey.SetActive(true);
    }

    public void AnimatorDragon(string animation)
    {
        anim.SetTrigger(animation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("InBaseArea"))
        {
            GetComponent<LookEvent>().LookAtObj = LookBase;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("InBaseArea"))
        {
            GetComponent<LookEvent>().LookAtObj = LookHome;
        }
    }
}
