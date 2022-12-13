using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BlackList : MonoBehaviour
{

    public List<GameObject> EnemyBlackList;
    [SerializeField] GunControl Gun;
    public List<GameObject> FlyEnemyBlackList;


    private void LateUpdate()
    {
        if (EnemyBlackList.Find((x) => x.CompareTag("Dead") || !x.gameObject.activeSelf))
        {
            var deadEnemy = EnemyBlackList.Find((x) => x.CompareTag("Dead"));
            EnemyBlackList.Remove(deadEnemy);
            Gun.ListRefresh();
        }

        if (FlyEnemyBlackList.Find((x) => x.CompareTag("Dead")))
        {
            var deadEnemy = FlyEnemyBlackList.Find((x) => x.CompareTag("Dead"));
            FlyEnemyBlackList.Remove(deadEnemy);
            Gun.ListRefresh();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            if (!EnemyBlackList.Contains(other.gameObject) && !other.gameObject.CompareTag("Dragon"))
            {
                EnemyBlackList.Add(other.gameObject);
                Gun.FindNewEnemy();
            }


            if (!FlyEnemyBlackList.Contains(other.gameObject) && other.gameObject.CompareTag("Dragon"))
            {
                FlyEnemyBlackList.Add(other.gameObject);
                Gun.FindNewEnemy();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            if (!EnemyBlackList.Contains(other.gameObject) && !other.gameObject.CompareTag("Dragon"))
            {
                EnemyBlackList.Add(other.gameObject);
                Gun.FindNewEnemy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (FlyEnemyBlackList.Contains(other.gameObject) && other.gameObject.CompareTag("Dragon"))
        {
            FlyEnemyBlackList.Remove(other.gameObject);
            Gun.FindNewEnemy();
        }
    }
}
