using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotControl : MonoBehaviour
{

    [SerializeField] List<GameObject> ThisBlackList;
    [SerializeField] List<GameObject> ThisFlyBlackList;
    [SerializeField] BlackList BlackList;
    [SerializeField] GunControl Gun;
    [SerializeField] ArmyMovement Army;

    private void Awake()
    {
        Army.speed = 0.05f;
        Army.LookEvent.RotateSpeed = 30f;
    }

    void Start()
    {
        ThisBlackList = BlackList.EnemyBlackList;
        ThisFlyBlackList = BlackList.FlyEnemyBlackList;
    }

    void LateUpdate()
    {
        if (ThisFlyBlackList.Count > 0)
        {

            if(!GetComponent<GunControl>().enabled)
            {
                GetComponent<GunControl>().enabled = true;

            }

            Gun.FireRobot();

        }

        else
        {

            if (ThisBlackList.Count > 0)
            {

                if (!GetComponent<GunControl>().enabled)
                {
                    GetComponent<GunControl>().enabled = true;

                }

                Gun.FireRobot();
            }

            else if (Army.LookEvent.LookAtObj == null)
            {
                Gun.FireStop();
            }
        }
    }
}
