using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    [SerializeField] GameObject DroneMachine, RobotMachine, TornadoMachine, NewGun1, NewGun2;
    [SerializeField] ProgressBarPro Bar;
    [SerializeField] Transform Habitants;
    [SerializeField] CardSpawnEvent Card;
    int countOurArmy = 0;
    int countRobot = 0;
    int countDrone = 0;

    void Start()
    {
        if (PlayerPrefs.GetInt("RobotMachine") == 1)
        {
            RobotMachine.SetActive(true);
        }

        if (PlayerPrefs.GetInt("DroneMachine") == 1)
        {
            DroneMachine.SetActive(true);
        }

        if (PlayerPrefs.GetInt("TornadoMachine") == 1)
        {
            TornadoMachine.SetActive(true);
        }

        if (PlayerPrefs.GetInt("NewGun") == 1)
        {
            NewGun1.SetActive(true);
            NewGun2.SetActive(true);
        }

        if (PlayerPrefs.GetInt("NotFirstGP") == 1) 
        Bar.Value = PlayerPrefs.GetFloat("CastleHealth");

        StartCoroutine(SpawnDelay());
     
    }

    IEnumerator SpawnDelay()
    {

        var Limit = 32 - (PlayerPrefs.GetInt("Drone") + PlayerPrefs.GetInt("Robot"));

        for (int i = 0; i < PlayerPrefs.GetInt("Drone"); i++)
        {
            if (i > 32)
            {
                break;
            }

            Card.DroneSave();
            yield return new WaitForSeconds(1f);

        }

        for (int i = 0; i < PlayerPrefs.GetInt("Robot"); i++)
        {
            if (i > 32)
            {
                break;
            }

            Card.RobotSave();
            yield return new WaitForSeconds(1f);

        }

        for (int i = 0; i < PlayerPrefs.GetInt("Army"); i++)
        {
            if (i > Limit)
            {
                break;
            }

            Card.ArmySave();
            yield return new WaitForSeconds(1f);

        }




    }

    private void OnApplicationQuit()
    {
        for(int i=0; i < Habitants.childCount; i++)
        {
            if (Habitants.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (Habitants.GetChild(i).gameObject.name == "Drone")
                {
                    countDrone += 1;
                    PlayerPrefs.SetInt("Drone", countDrone);
                }

                else if (Habitants.GetChild(i).gameObject.name == "Robot")
                {
                    countRobot += 1;
                    PlayerPrefs.SetInt("Robot", countRobot);
                }

                else
                {
                    countOurArmy += 1;
                    PlayerPrefs.SetInt("Army", countOurArmy);
                }
            }
        }

    }
}
