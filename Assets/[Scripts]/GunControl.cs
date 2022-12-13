using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEntityGun
{
    void Fire(Transform NewTarget);
    void FireStop();
    void FindNewEnemy();
    void MoneyCalculate(int fee);

}

public class GunControl : MonoBehaviour, IEntityGun
{

    #region InterfaceImplement

    IEntityGun interfaceEntity;

    public void FireStop()
    {
        FireParticle.Stop();
        if(gunBackMove != null)
        gunBackMove.SetBool("Fire", false);
        LookEvent(null);
        InFireTime = false;
        FindNewEnemy();
        time = false;
        StopCoroutine(BatteryLow());


        if (!oneShotForBattery)
        {
            oneShotForBattery = true;
            StopCoroutine(BatteryLow());

        }
    }

    public void Fire(Transform NewTarget)
    {
        if (batteryValue > 0)
        {
            FireParticle.Play();
            if (gunBackMove != null)
                gunBackMove.SetBool("Fire", true);
            LookEvent(NewTarget);
            if (oneShotForBattery)
            {
                oneShotForBattery = false;
                StartCoroutine(BatteryLow());
            }
            InFireTime = true;
        }

    }

    public void FindNewEnemy()
    {

        var newFlyEnemy = ThisFlyBlackList.Find((x) => x.CompareTag("Dragon"));
        var newEnemy = ThisBlackList.Find((x) => x.CompareTag("Live"));

        if (newFlyEnemy != null)
        {
            LookEvent(newFlyEnemy.transform);
            Fire(newFlyEnemy.transform);
        }

        else
        {
            if (newEnemy != null)
            {
                LookEvent(newEnemy.transform.GetChild(1).transform);
                Fire(newEnemy.transform.GetChild(1).transform);
            }
        }
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
    [SerializeField] ParticleSystem FireParticle;
    [SerializeField] Animator gunBackMove;
    [SerializeField] LookEvent LookGun, LookMotor;
    public GameObject[] BatteryDisc;
    public int batteryValue;
    [SerializeField] bool InFireTime;
    [SerializeField] List<GameObject> ThisBlackList;
    [SerializeField] List<GameObject> ThisFlyBlackList;
    [SerializeField] BlackList BlackList;

    bool oneShotForBattery = true;
    bool time;

    private void Start()
    {
        Economy = GameObject.FindWithTag("Economy");
        connectionEconomy = GameObject.FindWithTag("Economy").GetComponent<Economy.InGameEconomy>();
        interfaceEntity = GetComponent<IEntityGun>();
        batteryValue = BatteryDisc.Length -1;
        ThisBlackList = BlackList.EnemyBlackList;
        ThisFlyBlackList = BlackList.FlyEnemyBlackList;

        transform.parent.parent.name = this.gameObject.name;
    }


    public void FireRobot()
    {
        if(!FireParticle.isPlaying)
        FireParticle.Play();
        if (gunBackMove != null)
            gunBackMove.SetBool("Fire", true);
        InFireTime = true;


        if (ThisFlyBlackList.Count > 0 && ThisFlyBlackList[0] != null)
        {
            LookEvent(ThisFlyBlackList[0].transform);
        }

        else
        {
            if(ThisBlackList.Count > 0)
            LookEvent(ThisBlackList[0].transform);
        }

    }


    void LookEvent(Transform newTarget)
    {
        LookGun.LookAtObj = newTarget;
        LookGun.LookTarget = newTarget;
        LookMotor.LookTarget = newTarget;
        LookMotor.LookAtObj = newTarget;
    }

    public void ListRefresh()
    {

        if(ThisBlackList.Count == 0)
        {
            FireStop();
        }

        else
        {
            if(!InFireTime)
            FindNewEnemy();
        }
    }

    void LateUpdate()
    {
        if ((LookGun.LookTarget != null && !LookGun.LookTarget.gameObject.activeInHierarchy) || (LookGun.LookTarget != null && !LookGun.LookTarget.gameObject.activeSelf) || (LookGun.LookAtObj != null && !LookGun.LookAtObj.gameObject.activeInHierarchy) || (LookGun.LookAtObj != null && !LookGun.LookAtObj.gameObject.activeSelf) || (LookGun.LookAtObj != null && LookGun.LookAtObj.CompareTag("Dead")) || LookGun.LookAtObj == null)
        {
            LookGun.LookTarget = null;
            LookGun.LookAtObj = null;
            FireStop();
            FindNewEnemy();
        }

        if(batteryValue == 0 && InFireTime)
        {
            FireStop();

            foreach (GameObject child in BatteryDisc)
            {
                child.SetActive(false);
            }
        }

        if (!FireParticle.isPlaying)
        {
            FireStop();
        }
    }

    IEnumerator BatteryLow()
    {

        if (!time)
        {
            time = true;

            yield return new WaitForSeconds(11f);
            print("s");

            if (InFireTime)
            {

                if (BatteryDisc[batteryValue] != null && BatteryDisc[batteryValue].activeSelf)
                {
                    if (batteryValue > 0)
                    {
                        BatteryDisc[batteryValue].gameObject.SetActive(false);
                        StartCoroutine(BatteryLow());
                        batteryValue -= 1;
                    }

                    else
                    {
                        BatteryDisc[batteryValue].gameObject.SetActive(false);
                        FireStop();
                        StopCoroutine(BatteryLow());
                    }
                }
            }

            else
            {
                FireStop();
                StopCoroutine(BatteryLow());
            }

            time = false;
        }

    }
}
