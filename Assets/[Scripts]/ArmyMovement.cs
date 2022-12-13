using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IEntityArmy
{
    void HealthDamage(int damage);
    void Attack(GameObject Enemy);
    void Dead();
    void Run();
    void AfterAttack();
    void MoneyCalculate(int fee);

}

public class ArmyMovement : MonoBehaviour
{
    IEntityArmy interfaceEntity;

    #region InterfaceImplement

    public void HealthDamage(int damage)
    {
        HealthValue -= damage;

        if(name != "Drone")
        Mesh.DOShakeScale(0.1f, 0.1f, 3).OnComplete(() => Mesh.DOScale(Vector3.one, 0.1f));

        if (HealthValue <= 0)
        {
            Dead();
        }

    }

    public void Attack(GameObject Enemy)
    {
        safeTime = 0;
        speedInside = 0;
        if (anim != null)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Attack", true);
        }

        if(Enemy.GetComponent<EnemyMovement>())
        Enemy.GetComponent<EnemyMovement>().HealthDamage(DamageValue);

    }

    public void Dead()
    {
        if (!oneshotDead)
        {
            oneshotDead = true;

            if (anim != null)
                anim.SetBool("Dead", true);
            speed = 0;
            speedInside = 0;
            tag = "Dead";
            transform.parent = GarbageCollector;
            transform.DOScale(Vector3.zero, 1f).SetDelay(2f).OnComplete(() => this.gameObject.SetActive(false));
            transform.DOMoveY(-2, 1).SetDelay(2f);

            connectionEconomy.CalculateMoney(-1 * EnemyHitFee);


            MyFirstPoint.name = "Available";


            if (CurrentTarget != null)
                CurrentTarget.name = "Available";

        }

    }

    public void AfterAttack()
    {
        if (ThisBlackList.Count == 0)
        {
            speedInside = 0;

            if (anim != null && RobotAnim == null)
            {
                if(anim.GetBool("Attack"))
                anim.SetBool("Attack", false);
                if (!anim.GetBool("Idle"))
                anim.SetBool("Idle", true);
            }
        }
    }

    public void Run()
    {
        transform.Translate(Vector3.forward * speedInside);

        if (RobotAnim != null)
        {
            if (speedInside == 0)
            {
                if (RobotAnim.GetBool("Move"))
                    RobotAnim.SetBool("Move", false);

            }
            else
            {
                if (!RobotAnim.GetBool("Move"))
                RobotAnim.SetBool("Move", true);

            }
        }

        if(ThisBlackList.Count == 0 && (LookEvent.LookAtObj == null || !LookEvent.LookAtObj.gameObject.activeSelf))
        {
            AfterAttack();
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
    Animator anim;
    [SerializeField] Transform Mesh;
    public float speed;
    float speedInside;
    float safeTime;
    bool decision;
    public int HealthValue = 100;
    public int DamageValue;
    public int EnemyHitFee;
    bool oneshotDead;
    [SerializeField] List<GameObject> ThisBlackList;
    [SerializeField] BlackList BlackList;
    public LookEvent LookEvent;
    [SerializeField] Transform GarbageCollector;
    [SerializeField] Transform FirstPoints;
    [SerializeField] Transform MyFirstPoint;
    [SerializeField] GameObject CurrentTarget;
    public Animator RobotAnim;
    GameObject mesh;
    public bool İsRobot;

    int getNewTarget;

    void Start()
    {

        foreach (Transform child in FirstPoints)
        {
            if(child.name != "Reserved")
            {
                child.name = "Reserved";
                MyFirstPoint = child;
                LookEvent.LookAtObj = child;
                break;
            }
        }


        Economy = GameObject.FindWithTag("Economy");
        connectionEconomy = GameObject.FindWithTag("Economy").GetComponent<Economy.InGameEconomy>();
        interfaceEntity = GetComponent<IEntityArmy>();

        if (Mesh != null)
        {
            foreach (Transform child in Mesh)
            {
                if (child.gameObject.activeSelf)
                {
                    anim = child.GetComponent<Animator>();       
                }
            }
        }

        speedInside = speed;

        ThisBlackList = BlackList.EnemyBlackList;

    }

    void FixedUpdate()
    {
        Run();

        if (decision)
        {
            AfterAttack();
        }

        if (tag == "Tornado")
        {
            if (speedInside == 0)
            {
                FindNewEnemy();
            }
        }

        if (LookEvent.LookAtObj == null || LookEvent.LookAtObj.tag == "FirstArmyPos")
        {

            if (ThisBlackList.Count > 0)
            {
                 getNewTarget = Random.Range(0, ThisBlackList.Count);

                if (ThisBlackList[getNewTarget].name != "OnTarget")
                {
                    LookEvent.LookAtObj = ThisBlackList[getNewTarget].transform;
                    LookEvent.LookAtObj.name = "OnTarget";
                    CurrentTarget = ThisBlackList[getNewTarget].gameObject;

                    speedInside = speed;
                    if (anim != null && RobotAnim == null)
                    {
                        if(anim.GetBool("Idle"))
                        anim.SetBool("Idle", false);
                        if (anim.GetBool("Attack"))
                        anim.SetBool("Attack", false);
                    }

                    MyFirstPoint.name = "Available";

                    safeTime = 0;
                    decision = false;
                }
            }
        }
}

    public void FindNewEnemy()
    {
        var newEnemy = ThisBlackList.Find((x) => x.CompareTag("Live"));

        if (newEnemy != null)
        {
            LookEvent.LookAtObj = newEnemy.transform;
            speedInside = speed;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (LayerMask.NameToLayer("Enemy")))
        {
            Attack(other.gameObject);
        }

        if(other.gameObject == MyFirstPoint.gameObject && ThisBlackList.Count == 0)
        {
            speedInside = 0;
            anim.SetBool("Attack", false);
            anim.SetBool("Idle", true);
            LookEvent.LookAtObj = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (LayerMask.NameToLayer("Enemy")))
        {
            decision = true;
        }
    }
}
