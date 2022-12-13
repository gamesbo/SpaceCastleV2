using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IEntityEnemy
{
    void HealthDamage(int damage);
    void Attack(GameObject PlayerArmy);
    void Dead();
    void Run();
    void AfterAttack();
    void MoneyCalculate(int fee);

}

public class EnemyMovement : MonoBehaviour, IEntityEnemy
{
    IEntityEnemy interfaceEntity;

    #region InterfaceImplement

    public void HealthDamage(int damage)
    {
        HealthValue -= damage;
        Mesh.DOShakeScale(0.1f, 0.1f, 3).OnComplete(() => Mesh.DOScale(Vector3.one, 0.1f));
        if(name != "Giant")
        anim.SetTrigger("Hit");


        if (HealthValue <= 0)
        {
            Dead();
        }

    }

    public void Attack(GameObject PlayerArmy)
    {
        safeTime = 0;
        speedInside = 0;
        anim.SetBool("Attack", true);

        if(PlayerArmy != null && PlayerArmy.GetComponent<ArmyMovement>())
        PlayerArmy.GetComponent<ArmyMovement>().HealthDamage(DamageValue);

    }

    public void Dead()
    {

        if (!oneshotDead)
        {
            oneshotDead = true;

            anim.SetBool("Dead", true);
            speed = 0;
            speedInside = 0;
            gameObject.layer = 0;
            name = "Dead";
            tag = "Dead";
            transform.GetChild(1).tag = "Dead";
            transform.GetChild(1).gameObject.SetActive(false);

            transform.parent = GarbageCollector;
            transform.DOScale(Vector3.zero, 1f).SetDelay(4f).OnComplete(() => this.gameObject.SetActive(false));
            transform.DOMoveY(-2, 1).SetDelay(4f);

            connectionEconomy.CalculateMoney(EnemyHitFee);

        }

    }

    public void AfterAttack()
    {
        safeTime += Time.deltaTime;

        if (safeTime > 2.75f)
        {
            speedInside = speed;
            anim.SetBool("Attack", false);
            safeTime = 0;
            decision = false;
        }
    }

    public void Run()
    {

        transform.Translate(Vector3.forward * speedInside);


        if (name == "DragonsEnemy")
        {

            transform.Translate(Vector3.up * dragonYspeed / 10f);
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
    [SerializeField] Transform GarbageCollector;
    [SerializeField] Transform TornadoObject;
    [SerializeField] LookEvent LookEventScript;
    bool oneshotDead;
    Animator JumperAnim;
    public float speed;
    float speedInside;
    float safeTime;
    bool decision;
    public int HealthValue = 100;
    public int DamageValue;
    public int EnemyHitFee = 10;
    bool TornadoEffect;
    bool TornadoOneShot;
    float dragonYspeed;
    bool oneShotForDragonJump = true;
    public bool DragonsEnemy;

    void Start()
    {

        if(GetComponent<LookEvent>())
        {
            LookEventScript = GetComponent<LookEvent>();
        }

        if(this.gameObject.name == "Giant")
        {
            HealthValue = 2000;
        }

        Economy = GameObject.FindWithTag("Economy");
        connectionEconomy = GameObject.FindWithTag("Economy").GetComponent<Economy.InGameEconomy>();
        interfaceEntity = GetComponent<IEntityEnemy>();
        foreach(Transform child in Mesh)
        {
            if (child.gameObject.activeSelf)
                anim = child.GetComponent<Animator>();
        }

        speedInside = speed;

        dragonYspeed = speed;
    }

    void FixedUpdate()
    {
        if (TornadoEffect)
        {

            this.gameObject.transform.RotateAround(TornadoObject.position, Vector3.up, 350 * Time.deltaTime);


            var ey = transform.GetChild(0).position;
            var direction = (TornadoObject.position - ey).normalized;
            ey = ey + direction * 0.1f;



            var ey2 = transform.GetChild(0).position;
            var direction2 = (TornadoObject.position - ey).normalized;
            ey2 = ey2 + direction2 * 0.2f;

            transform.GetChild(0).position = new Vector3(ey.x, ey2.y, transform.GetChild(0).position.z);

        }

        else
        {
            Run();

            if (decision)
            {
                AfterAttack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (LayerMask.NameToLayer("Player")))
        {
            if (other.CompareTag("Tornado"))
            {
                if (!TornadoOneShot)
                {

                    if(this.gameObject.name == "Giant")
                    {
                        transform.DOScale(Vector3.one * 0.85f, 1f);
                    }        

                    GetComponent<Collider>().enabled = false;
                    GetComponent<Collider>().isTrigger = false;
                    TornadoEffect = true;
                    TornadoObject = other.gameObject.transform.GetChild(1);
                    LookEventScript.LookAtObj = other.gameObject.transform;
                    LookEventScript.LookTarget = other.gameObject.transform;

                    speed = 0;
                    speedInside = 0;
                    tag = "Dead";
                    transform.GetChild(1).tag = "Dead";
                    transform.parent = GarbageCollector;

                    StartCoroutine(ThrowTornado());

                }

            }

            else
            {
                if(!TornadoOneShot)
                Attack(other.gameObject);
            }
        }

        if (other.gameObject.layer != (LayerMask.NameToLayer("Player")))
        {
            if (!TornadoOneShot)
                decision = true;
        }

        if(other.CompareTag("JumperStoper"))
        {
            if (name == "DragonsEnemy")
            {
                speedInside = 0;
                dragonYspeed = 0;
                anim.SetBool("Attack2", true);
            }
        }

        if(other.CompareTag("Jump") && DragonsEnemy)
        {

            if (oneShotForDragonJump)
            {
                other.gameObject.SetActive(false);

                foreach (Transform child in transform.GetChild(0))
                {
                    if (child.gameObject.activeSelf)
                    {
                        JumperAnim = child.gameObject.GetComponent<Animator>();
                        break;
                    }
                }

                transform.parent = null;
                JumperAnim.SetTrigger("Jump");
                GetComponent<EnemyMovement>().enabled = true;
                transform.GetChild(0).DOLocalJump(new Vector3(-0.285f, -0.285f, -0.85f), 2, 1, 1);
                transform.GetChild(0).DOLocalRotate(new Vector3(-67.947f, -67.947f, 79.221f), 1);
                transform.DOScale(Vector3.one * 3, 1);

                StartCoroutine(DelayJump(other.gameObject));
                oneShotForDragonJump = false;
            }
        }
    }


    IEnumerator DelayJump(GameObject other)
    {
        yield return new WaitForSeconds(4f);
        other.gameObject.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (LayerMask.NameToLayer("Player")))
        {
            decision = true;
        }
    }

    IEnumerator ThrowTornado()
    {
        yield return new WaitForSeconds(3f);
        TornadoEffect = false;
        speedInside = 0;
        transform.GetChild(0).DOLocalMove(Vector3.zero, 0.25f);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(-1 * transform.right * 500);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
        GetComponent<Rigidbody>().AddTorque(-1 * transform.right * 100);


        Dead();


    }
}
