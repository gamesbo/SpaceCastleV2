using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardSpawnEvent : MonoBehaviour
{
    Transform newP;
    [SerializeField] Transform SpawnPoints;
    [SerializeField] Transform SpawnPoints2;
    [SerializeField] Transform SpawnPoints3;
    [SerializeField] Transform SpawnPoints4;
    [SerializeField] GameObject AIMesh;
    [SerializeField] Transform OurArmySpawnPoints;
    [SerializeField] GameObject OurArmyMesh;
    [SerializeField] GunControl Gun;
    public int PowerFee;
    GameObject Economy;
    Economy.InGameEconomy connectionEconomy;
    [SerializeField] Animator SpawnMachineAnim, SpawnMachineAnim2, SpawnMachineAnim3;
    [SerializeField] GameObject Dragon;
    [SerializeField] GameObject Dragon2Mesh;
    [SerializeField] Animator Dragon2anim;
    [SerializeField] ParticleSystem BoneShield;
    [SerializeField] ParticleSystem Lava;
    [SerializeField] ParticleSystem Trap;
    [SerializeField] GameObject Tornado;
    [SerializeField] GameObject Bird;
    [SerializeField] Transform FirstArmyControl;
    [SerializeField] GameObject Dragon2;
    [SerializeField] Animator Cannon, Cannon2, Cannon3;
    Animator CannonAnim;
    [SerializeField] Animator TornadoMachine;
    [SerializeField] ParticleSystem Lighting, Lighting2;
    [SerializeField] GameObject Step2MachineGunObj1, Step2MachineGunObj2;
    [SerializeField] Transform Base;
    [SerializeField] Animator BaseEffect;
    [SerializeField] Transform SpawnMachinePos1, SpawnMachinePos2, SpawnMachinePos3;
    Animator CurrentMachineAnim;
    [SerializeField] Transform SubCards;
    [SerializeField] Transform MainCards;
    [SerializeField] Animator WaveAnim;
    [SerializeField] Text WaveText;
    [SerializeField] ParticleSystem Shake;
    [SerializeField] GameObject BuyGun, BuyTornado, BuyDrone, BuyRobot;
    [SerializeField] Transform InHabitants;
    [SerializeField] ParticleSystem Mac1Part, Mac2Part, Mac3Part;
    bool controlEnd;
    float shake = 0.25f;
    float during = 0.5f;
    int vibr = 2;
    [SerializeField] bool CreativeSpawn;

    void CardFeeControl()
    {
        if(controlEnd)
        {
            for(int i=0; i< InHabitants.childCount; i++)
            {
                if(InHabitants.GetChild(i).gameObject.activeSelf)
                {
                    break;
                }

                if(i == InHabitants.childCount -1)
                {
                    EKTemplate.LevelManager.instance.Success();
                    Haptic.NotificationSuccessTaptic();
                    controlEnd = false;
                    break;
                }
            }
        }

        for (int i = 0; i < SubCards.childCount; i++)
        {
            for (int j = 0; j < SubCards.transform.GetChild(i).childCount; j++)
            {
                if (connectionEconomy.TotalMoney >= int.Parse(SubCards.transform.GetChild(i).GetChild(j).GetChild(2).GetComponent<Text>().text))
                {


                    if (SubCards.transform.GetChild(i).GetChild(j).transform.localScale.x != 1)
                    {
                        SubCards.transform.GetChild(i).GetChild(j).transform.DOScale(Vector3.one, 0.2f);

                    }



                    foreach (Transform child in MainCards)
                    {
                        if(SubCards.transform.GetChild(i).name == child.gameObject.name)
                        {
                            child.transform.GetChild(1).gameObject.SetActive(true);
                        }

                    }


                    if (!SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.activeSelf)
                    {
                        if(SubCards.transform.GetChild(i).GetChild(j).gameObject.name == "Tornado" && TornadoMachine.gameObject.activeSelf)
                        {

                            SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(false);
                            break;

                        }

                        else if (SubCards.transform.GetChild(i).GetChild(j).gameObject.name == "Robot" && SpawnMachineAnim2.gameObject.activeSelf)
                        {

                            SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(false);
                            break;

                        }

                        else if (SubCards.transform.GetChild(i).GetChild(j).gameObject.name == "Drone" && SpawnMachineAnim3.gameObject.activeSelf)
                        {
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(false);
                            break;

                        }

                        else if (SubCards.transform.GetChild(i).GetChild(j).gameObject.name != "Drone" &&
                            SubCards.transform.GetChild(i).GetChild(j).gameObject.name != "Robot" &&
                           SubCards.transform.GetChild(i).GetChild(j).gameObject.name != "Tornado")
                        {
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                            SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(false);

                            break;

                        }

                    }

                    else
                    {
                        SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(false);
                    }

                }

                else
                {
                    if (SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.activeSelf)
                    {
                        SubCards.transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(false);
                        SubCards.transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(true);
                    }

                    if (SubCards.transform.GetChild(i).GetChild(j).transform.localScale.x != 0.85f)
                        SubCards.transform.GetChild(i).GetChild(j).transform.DOScale(Vector3.one * 0.85f, 0.2f);



                    if(j == SubCards.transform.GetChild(i).childCount-1)
                    {
                        foreach (Transform child in MainCards)
                        {
                            if (SubCards.transform.GetChild(i).name == child.gameObject.name)
                            {
                                if (child.transform.GetChild(1).gameObject.activeSelf)
                                child.transform.GetChild(1).gameObject.SetActive(false);
                            }

                        }
                    }
                }
            }
        }
    }

    public void Clear()
    {
        foreach (Transform child in SubCards)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in MainCards)
        {
            child.transform.DOScale(Vector3.one, 0.2f);
        }
    }

    public void OpenClose(GameObject SubCard)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);

        Haptic.MediumTaptic();

        foreach (Transform child in SubCards)
        {
            if(child.gameObject != SubCard)
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in MainCards)
        {
            if (child.gameObject != CurrentButton.gameObject)
                child.transform.DOScale(Vector3.one * 0.75f, 0.2f);
        }

        if (SubCard != null)
        {

            if (!SubCard.activeSelf)
            {
                SubCard.SetActive(true);
            }

            else
            {
                SubCard.SetActive(false);

                foreach (Transform child in MainCards)
                {
                    child.transform.DOScale(Vector3.one, 0.2f);
                }
            }
        }   

    }


    public void LightinAttack(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);

        var CardFee = int.Parse(Fee.text);
        
        if(connectionEconomy.TotalMoney>=  CardFee)
        {
            if (!Lighting.isPlaying)
            {
                Lighting.Play();
                Lighting2.Play();
                connectionEconomy.CalculateMoney(-1 * CardFee);
                Shake.gameObject.SetActive(true);
            }
        }
    }

    public void BaseRepair(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            BaseEffect.SetTrigger("Effect");
            StartCoroutine(DelayBaseRecovery());
            connectionEconomy.CalculateMoney(-1 * CardFee);

        }
    }

    public void BuyNewGun(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!Step2MachineGunObj1.gameObject.activeSelf)
            {
                Step2MachineGunObj1.SetActive(true);
                Step2MachineGunObj2.SetActive(true);
                connectionEconomy.CalculateMoney(-1 * CardFee);



                BuyGun.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                BuyGun.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;

                BuyGun.gameObject.transform.GetChild(1).gameObject.SetActive(true);

                BuyGun.gameObject.transform.GetChild(2).GetComponent<Text>().enabled = false;
                BuyGun.gameObject.transform.GetChild(2).GetComponent<Text>().text = "9999999";

                BuyGun.gameObject.transform.GetChild(3).gameObject.SetActive(true);

                PlayerPrefs.SetInt("NewGun", 1);

            }
        }
    }

    public void BoneDefend(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!BoneShield.isPlaying)
            {
                BoneShield.Play();
                connectionEconomy.CalculateMoney(-1 * CardFee);

                Shake.gameObject.SetActive(true);


            }
        }
    }

    public void TrapDefend(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!Trap.isPlaying)
            {
                Trap.Play();

                connectionEconomy.CalculateMoney(-1 * CardFee);

                Shake.gameObject.SetActive(true);


            }
        }
    }

    public void TornadoAttack(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (TornadoMachine.gameObject.activeSelf)
            {
                TornadoMachine.SetTrigger("Generate");
                var ey = Instantiate(Tornado, Tornado.transform.position, Tornado.transform.rotation, null);
                ey.SetActive(true);
                StartCoroutine(TornadoDelay(ey.gameObject));

                connectionEconomy.CalculateMoney(-1 * CardFee);

            }
        }
    }

    public void LavaAttack(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!Lava.isPlaying)
            {
                Lava.Play();

                connectionEconomy.CalculateMoney(-1 * CardFee);

            }
        }
    }

    public void OurArmyGenerate(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            foreach (Transform child in FirstArmyControl)
            {
                if (child.name != "Reserved")
                {
                    var mesh = Random.Range(0, OurArmyMesh.transform.GetChild(0).childCount - 2);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos1.position, SpawnMachinePos1.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    SpawnMachineAnim.SetTrigger("Generate");
                    enemy.SetActive(true);
                    connectionEconomy.CalculateMoney(-1 * CardFee);

                    if (!Mac1Part.isPlaying) Mac1Part.Play();

                    break;

                }
            }
        }
    }

    public void RobotGenerate(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            foreach (Transform child in FirstArmyControl)
            {
                if (child.name != "Reserved")
                {
                    var mesh = Random.Range(1, OurArmyMesh.transform.GetChild(0).childCount - 1);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos2.position, SpawnMachinePos2.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.GetComponent<ArmyMovement>().İsRobot = true;
                    enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                    enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    SpawnMachineAnim2.SetTrigger("Generate");
                    enemy.SetActive(true);
                    connectionEconomy.CalculateMoney(-1 * CardFee);
                    if (!Mac2Part.isPlaying) Mac2Part.Play();


                    break;

                }
            }
        }
    }


    public void DroneGenerate(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            foreach (Transform child in FirstArmyControl)
            {
                if (child.name != "Reserved")
                {
                    var mesh = Random.Range(2, OurArmyMesh.transform.GetChild(0).childCount);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos3.position, SpawnMachinePos3.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 2).SetDelay(2.5f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.GetComponent<ArmyMovement>().İsRobot = true;
                    enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                    enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;
                    scaleMesh.transform.DOScale(Vector3.one, 1).SetDelay(2);
                    SpawnMachineAnim3.SetTrigger("Generate");
                    enemy.SetActive(true);
                    connectionEconomy.CalculateMoney(-1 * CardFee);

                    if (!Mac3Part.isPlaying) Mac3Part.Play();

                    break;

                }
            }
        }
    }


    public void BatterUp(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            for (int i = 0; i <= Gun.BatteryDisc.Length - 1; i++)
            {
                if (!Gun.BatteryDisc[i].gameObject.activeSelf)
                {
                    Gun.BatteryDisc[i].gameObject.SetActive(true);
                    Gun.batteryValue = Mathf.Clamp(Gun.batteryValue += 1, 0, 7);
                    Gun.FindNewEnemy();
                    Haptic.MediumTaptic();
                    connectionEconomy.CalculateMoney(-1 * CardFee);
                    break;
                }
            }
        }
    }


    public void BuyTornadoMachine(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if(!TornadoMachine.gameObject.activeSelf)
            {
                TornadoMachine.gameObject.SetActive(true);

                connectionEconomy.CalculateMoney(-1 * CardFee);

            }

            BuyTornado.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            BuyTornado.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;

            BuyTornado.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            BuyTornado.gameObject.transform.GetChild(2).GetComponent<Text>().enabled = false;
            BuyTornado.gameObject.transform.GetChild(2).GetComponent<Text>().text = "9999999";

            BuyTornado.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            PlayerPrefs.SetInt("TornadoMachine", 1);
        }
    }

    public void BuyDroneMachine(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!SpawnMachineAnim3.gameObject.activeSelf)
            {
                SpawnMachineAnim3.gameObject.SetActive(true);

                connectionEconomy.CalculateMoney(-1 * CardFee);

            }

            BuyDrone.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            BuyDrone.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;

            BuyDrone.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            BuyDrone.gameObject.transform.GetChild(2).GetComponent<Text>().enabled = false;
            BuyDrone.gameObject.transform.GetChild(2).GetComponent<Text>().text = "9999999";

            BuyDrone.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            PlayerPrefs.SetInt("DroneMachine", 1);

        }
    }


    public void BuyRobotMachine(Text Fee)
    {
        var CurrentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        CurrentButton.transform.DOScale(Vector3.one, 0);
        CurrentButton.transform.DOShakeScale(shake, during, vibr);
        var CardFee = int.Parse(Fee.text);

        if (connectionEconomy.TotalMoney >= CardFee)
        {
            if (!SpawnMachineAnim2.gameObject.activeSelf)
            {
                SpawnMachineAnim2.gameObject.SetActive(true);

                connectionEconomy.CalculateMoney(-1 * CardFee);

            }

            BuyRobot.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            BuyRobot.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;

            BuyRobot.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            BuyRobot.gameObject.transform.GetChild(2).GetComponent<Text>().enabled = false;
            BuyRobot.gameObject.transform.GetChild(2).GetComponent<Text>().text = "9999999";

            BuyRobot.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            PlayerPrefs.SetInt("RobotMachine", 1);

        }
    }


    public void OneGiantCall()
    {
        var getVal = Random.Range(1, 5);

        if (getVal == 1) newP = SpawnPoints;
        if (getVal == 2) newP = SpawnPoints2;
        if (getVal == 3) newP = SpawnPoints3;
        if (getVal == 4) newP = SpawnPoints4;



        var mesh = AIMesh.transform.GetChild(0).childCount - 1;
        var point = Random.Range(0, newP.childCount);
        var enemy = Instantiate(AIMesh, newP.GetChild(point).position, newP.GetChild(point).rotation, InHabitants);
        enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
        var scaleMesh = enemy.transform.GetChild(0).gameObject;
        enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
        scaleMesh.transform.DOScale(Vector3.zero, 0);
        scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
        scaleMesh.transform.DOScale(Vector3.one, 0.25f);
        scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

        enemy.SetActive(true);

        Shake.gameObject.SetActive(true);

    }

    public void OneEnemy()
    {
        foreach (Transform child in SpawnPoints)
        {
            if (child.name != "Reserved")
            {
                child.name = "Reserved";

                var getVal = Random.Range(1, 5);

                if (getVal == 1) newP = SpawnPoints;
                if (getVal == 2) newP = SpawnPoints2;
                if (getVal == 3) newP = SpawnPoints3;
                if (getVal == 4) newP = SpawnPoints4;

                var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                var point = Random.Range(0, newP.childCount);
                var enemy = Instantiate(AIMesh, newP.GetChild(point).position, newP.GetChild(point).rotation, InHabitants);
                enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

                enemy.SetActive(true);


                break;
            }
        }

        StartCoroutine(DelayEnemyPos(newP));
    }


    public void BigEnemyLeft()
    {
        foreach (Transform child in SpawnPoints)
        {
            if (child.name != "Reserved")
            {
                child.name = "Reserved";
                var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                var point = child.GetSiblingIndex();
                var enemy = Instantiate(AIMesh, SpawnPoints.GetChild(point).position, SpawnPoints.GetChild(point).rotation, InHabitants);
                enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

                enemy.SetActive(true);

                //break;
            }
        }

        StartCoroutine(DelayEnemyPos(SpawnPoints));
    }

    public void BigEnemyRight()
    {
        foreach (Transform child in SpawnPoints2)
        {
            if (child.name != "Reserved")
            {
                child.name = "Reserved";
                var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                var point = child.GetSiblingIndex();
                var enemy = Instantiate(AIMesh, SpawnPoints2.GetChild(point).position, SpawnPoints2.GetChild(point).rotation, InHabitants);
                enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                enemy.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                //break;
            }
        }

        StartCoroutine(DelayEnemyPos(SpawnPoints2));
    }

    public void BigEnemyRightFront()
    {
        foreach (Transform child in SpawnPoints3)
        {
            if (child.name != "Reserved")
            {
                child.name = "Reserved";
                var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                var point = child.GetSiblingIndex();
                var enemy = Instantiate(AIMesh, SpawnPoints3.GetChild(point).position, SpawnPoints3.GetChild(point).rotation, InHabitants);
                enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                enemy.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                //break;
            }
        }

        StartCoroutine(DelayEnemyPos(SpawnPoints3));
    }

    public void BigEnemyLeftFront()
    {
        foreach (Transform child in SpawnPoints4)
        {
            if (child.name != "Reserved")
            {
                child.name = "Reserved";
                var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                var point = child.GetSiblingIndex();
                var enemy = Instantiate(AIMesh, SpawnPoints4.GetChild(point).position, SpawnPoints4.GetChild(point).rotation, InHabitants);
                enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                enemy.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                //break;
            }
        }

        StartCoroutine(DelayEnemyPos(SpawnPoints4));
    }

    public void Dragon1Call()
    {
        Dragon.transform.parent = InHabitants;
        Dragon.SetActive(true);
    }

    public void Dragon2Call()
    {
        Dragon2.transform.parent = InHabitants;
        Dragon2.gameObject.SetActive(true);
    }

    public void CannonCall()
    {
        var getVal = Random.Range(1, 4);

        if (getVal == 1) CannonAnim = Cannon;
        if (getVal == 2) CannonAnim = Cannon2;
        if (getVal == 3) CannonAnim = Cannon3;

        CannonAnim.SetTrigger("Shot");

        Shake.gameObject.SetActive(true);

    }

    public void BirdCall()
    {
        if (!Bird.transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
        {
            Bird.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Bird.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        }
    }

    public void ArmySave()
    {

        foreach (Transform child in FirstArmyControl)
        {
            if (child.name != "Reserved")
            {



                var mesh = Random.Range(0, OurArmyMesh.transform.GetChild(0).childCount - 2);
                var point = Random.Range(0, OurArmySpawnPoints.childCount);
                var enemy = Instantiate(OurArmyMesh, SpawnMachinePos1.position, SpawnMachinePos1.rotation, InHabitants);
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                SpawnMachineAnim.SetTrigger("Generate");

                enemy.SetActive(true);


                break;

            }
        }
    }


    public void RobotSave()
    {
        foreach (Transform child in FirstArmyControl)
        {
            if (child.name != "Reserved")
            {
                var mesh = Random.Range(1, OurArmyMesh.transform.GetChild(0).childCount - 1);
                var point = Random.Range(0, OurArmySpawnPoints.childCount);
                var enemy = Instantiate(OurArmyMesh, SpawnMachinePos2.position, SpawnMachinePos2.rotation, InHabitants);
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                enemy.GetComponent<ArmyMovement>().İsRobot = true;
                enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;

                scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                SpawnMachineAnim2.SetTrigger("Generate");
                enemy.SetActive(true);


                break;

            }
        }
    }

    public void DroneSave()
    {
        foreach (Transform child in FirstArmyControl)
        {
            if (child.name != "Reserved")
            {
                var mesh = Random.Range(2, OurArmyMesh.transform.GetChild(0).childCount);
                var point = Random.Range(0, OurArmySpawnPoints.childCount);
                var enemy = Instantiate(OurArmyMesh, SpawnMachinePos3.position, SpawnMachinePos3.rotation, InHabitants);
                var scaleMesh = enemy.transform.GetChild(0).gameObject;
                enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 2).SetDelay(2.5f);
                scaleMesh.transform.DOScale(Vector3.zero, 0);
                scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                enemy.GetComponent<ArmyMovement>().İsRobot = true;
                enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;
                scaleMesh.transform.DOScale(Vector3.one, 1).SetDelay(2);
                SpawnMachineAnim3.SetTrigger("Generate");
                enemy.SetActive(true);


                break;

            }
        }
    }


    void Start()
    {
        Economy = GameObject.FindWithTag("Economy");
        connectionEconomy = GameObject.FindWithTag("Economy").GetComponent<Economy.InGameEconomy>();
        Application.targetFrameRate = 60;
        Time.timeScale = 0;

        if (!CreativeSpawn)
            StartCoroutine(GenerateEnemy());
    }


    public IEnumerator GenerateEnemy()
    {
        if (PlayerPrefs.GetInt("Wave") == 0)
        {
            yield return new WaitForSeconds(10f);
            WaveText.text = "1";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(5f);
            OneEnemy();
            yield return new WaitForSeconds(5f);
            OneEnemy();
            yield return new WaitForSeconds(4f);
            OneEnemy();
            yield return new WaitForSeconds(3f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            PlayerPrefs.SetInt("Wave", 1);
        }
        yield return new WaitForSeconds(5f);
        if (PlayerPrefs.GetInt("Wave") == 1)
        {
            WaveText.text = "2";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(5f);
            BigEnemyLeft();
            yield return new WaitForSeconds(25f);
            BigEnemyLeft();
            PlayerPrefs.SetInt("Wave", 2);
        }
        if (PlayerPrefs.GetInt("Wave") == 2)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();

            yield return new WaitForSeconds(10f);
            WaveText.text = "3";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(5f);
            BigEnemyRight();
            yield return new WaitForSeconds(25f);
            BigEnemyLeft();
            yield return new WaitForSeconds(20f);
            BigEnemyRight();
            yield return new WaitForSeconds(15f);
            CannonCall();
            PlayerPrefs.SetInt("Wave", 3);
        }
        if (PlayerPrefs.GetInt("Wave") == 3)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(10f);
            WaveText.text = "4";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(5f);
            BigEnemyLeftFront();
            yield return new WaitForSeconds(25f);
            BigEnemyRightFront();
            yield return new WaitForSeconds(15f);
            CannonCall();
            yield return new WaitForSeconds(12f);
            CannonCall();
            PlayerPrefs.SetInt("Wave", 4);
        }
        if (PlayerPrefs.GetInt("Wave") == 4)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(20f);
            WaveText.text = "5";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(5f);
            BigEnemyLeftFront();
            OneGiantCall();
            yield return new WaitForSeconds(10f);
            OneGiantCall();
            yield return new WaitForSeconds(1f);
            CannonCall();
            yield return new WaitForSeconds(10f);
            BigEnemyLeft();
            BigEnemyRightFront();
            OneGiantCall();
            yield return new WaitForSeconds(5f);
            Dragon1Call();
            PlayerPrefs.SetInt("Wave", 5);
        }
        if (PlayerPrefs.GetInt("Wave") == 5)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(20f);
            WaveText.text = "6";
            WaveAnim.SetTrigger("Visible");
            Dragon1Call();
            yield return new WaitForSeconds(5f);
            CannonCall();
            OneGiantCall();
            yield return new WaitForSeconds(5f);
            BigEnemyRight();
            yield return new WaitForSeconds(20f);
            BigEnemyLeft();
            yield return new WaitForSeconds(20f);
            BigEnemyRight();
            yield return new WaitForSeconds(20f);
            BigEnemyLeft();
            OneGiantCall();
            PlayerPrefs.SetInt("Wave", 6);
        }
        if (PlayerPrefs.GetInt("Wave") == 6)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(15f);
            WaveText.text = "7";
            WaveAnim.SetTrigger("Visible");
            Dragon1Call();
            yield return new WaitForSeconds(5f);
            BirdCall();
            OneGiantCall();
            yield return new WaitForSeconds(20f);
            BigEnemyRightFront();
            yield return new WaitForSeconds(20f);
            BigEnemyLeftFront();
            OneGiantCall();
            yield return new WaitForSeconds(20f);
            Dragon2Call();
            PlayerPrefs.SetInt("Wave", 7);
        }
        if (PlayerPrefs.GetInt("Wave") == 7)
        {
            WaveText.text = "8";
            WaveAnim.SetTrigger("Visible");
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(15f);
            WaveText.text = "8";
            WaveAnim.SetTrigger("Visible");
            Dragon1Call();
            yield return new WaitForSeconds(5f);
            Dragon2Call();
            BigEnemyLeftFront();
            OneGiantCall();
            BigEnemyRight();
            yield return new WaitForSeconds(40f);
            BigEnemyRightFront();
            OneGiantCall();
            BigEnemyLeft();
            PlayerPrefs.SetInt("Wave", 8);
        }
        if (PlayerPrefs.GetInt("Wave") == 8)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(35f);
            WaveText.text = "9";
            WaveAnim.SetTrigger("Visible");

            Dragon1Call();
            Dragon2Call();
            yield return new WaitForSeconds(5f);

            BigEnemyLeft();
            yield return new WaitForSeconds(3f);
            BigEnemyRight();
            yield return new WaitForSeconds(3f);
            BigEnemyRightFront();
            yield return new WaitForSeconds(3f);
            BigEnemyLeftFront();
            PlayerPrefs.SetInt("Wave", 9);
        }
        if (PlayerPrefs.GetInt("Wave") >= 9)
        {
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(2f);
            OneEnemy();
            yield return new WaitForSeconds(1f);
            OneEnemy();
            yield return new WaitForSeconds(35f);
            WaveText.text = "10";
            WaveAnim.SetTrigger("Visible");
            Dragon1Call();
            Dragon2Call();
            yield return new WaitForSeconds(5f);
            BirdCall();
            BigEnemyLeft();
            OneGiantCall();
            yield return new WaitForSeconds(3f);
            BigEnemyRight();
            OneGiantCall();
            yield return new WaitForSeconds(3f);
            BigEnemyRightFront();
            OneGiantCall();
            yield return new WaitForSeconds(3f);
            BigEnemyLeftFront();
            OneGiantCall();
            yield return new WaitForSeconds(20f);
            controlEnd = true;
            PlayerPrefs.SetInt("Wave", 10);
        }
    }

    IEnumerator TornadoDelay(GameObject tornado)
    {
        yield return new WaitForSeconds(5f);
        tornado.tag = "Untagged";
        yield return new WaitForSeconds(8f);
        tornado.SetActive(false);
    }

    IEnumerator DelayEnemyPos(Transform SpawnPoint)
    {
        yield return new WaitForSeconds(3f);
        foreach (Transform child in SpawnPoint)
        {
            child.name = "Available";
        }
    }

    IEnumerator DelayBaseRecovery()
    {
        yield return new WaitForSeconds(1);
        foreach (Transform child in Base)
        {
            child.gameObject.SetActive(true);
            child.name = "100";
        }
    }

    void Update()
    {

        CardFeeControl();


        if (Input.GetKeyDown(KeyCode.C))
        {
            BaseEffect.SetTrigger("Effect");
            StartCoroutine(DelayBaseRecovery());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Step2MachineGunObj1.SetActive(true);
            Step2MachineGunObj2.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!Lighting.isPlaying)
            {
                Lighting.Play();
                Lighting2.Play();
            }

            Shake.gameObject.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.A)) //Cannon
        {

            var getVal = Random.Range(1, 4);

            if (getVal == 1) CannonAnim = Cannon;
            if (getVal == 2) CannonAnim = Cannon2;
            if (getVal == 3) CannonAnim = Cannon3;

            CannonAnim.SetTrigger("Shot");

            Shake.gameObject.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.E)) //enemy generate
        {

            foreach (Transform child in SpawnPoints)
            {
                if (child.name != "Reserved")
                {
                    child.name = "Reserved";
                    var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                    var point = child.GetSiblingIndex();
                    var enemy = Instantiate(AIMesh, SpawnPoints.GetChild(point).position, SpawnPoints.GetChild(point).rotation, InHabitants);
                    enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

                    enemy.SetActive(true);

                    //break;
                }
            }

            StartCoroutine(DelayEnemyPos(SpawnPoints));
        
        }


        if (Input.GetKeyDown(KeyCode.H)) //enemy generate right
        {

            foreach (Transform child in SpawnPoints2)
            {
                if (child.name != "Reserved")
                {
                    child.name = "Reserved";
                    var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                    var point = child.GetSiblingIndex();
                    var enemy = Instantiate(AIMesh, SpawnPoints2.GetChild(point).position, SpawnPoints2.GetChild(point).rotation, InHabitants);
                    enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    //break;
                }
            }

            StartCoroutine(DelayEnemyPos(SpawnPoints2));

        }


        if (Input.GetKeyDown(KeyCode.J)) //enemy generate front
        {

            foreach (Transform child in SpawnPoints3)
            {
                if (child.name != "Reserved")
                {
                    child.name = "Reserved";
                    var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                    var point = child.GetSiblingIndex();
                    var enemy = Instantiate(AIMesh, SpawnPoints3.GetChild(point).position, SpawnPoints3.GetChild(point).rotation, InHabitants);
                    enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    //break;
                }
            }

            StartCoroutine(DelayEnemyPos(SpawnPoints3));

        }


        if (Input.GetKeyDown(KeyCode.K)) //enemy generate front
        {

            foreach (Transform child in SpawnPoints4)
            {
                if (child.name != "Reserved")
                {
                    child.name = "Reserved";
                    var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                    var point = child.GetSiblingIndex();
                    var enemy = Instantiate(AIMesh, SpawnPoints4.GetChild(point).position, SpawnPoints4.GetChild(point).rotation, InHabitants);
                    enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    //break;
                }
            }

            StartCoroutine(DelayEnemyPos(SpawnPoints4));

        }

        if (Input.GetKeyDown(KeyCode.W)) //enemy 1 generate
        {

            foreach (Transform child in SpawnPoints)
            {
                if (child.name != "Reserved")
                {
                    child.name = "Reserved";

                    var getVal = Random.Range(1, 5);

                    if (getVal == 1) newP = SpawnPoints;
                    if (getVal == 2) newP = SpawnPoints2;
                    if (getVal == 3) newP = SpawnPoints3;
                    if (getVal == 4) newP = SpawnPoints4;

                    var mesh = Random.Range(0, AIMesh.transform.GetChild(0).childCount - 1);
                    var point = Random.Range(0, newP.childCount);
                    var enemy = Instantiate(AIMesh, newP.GetChild(point).position, newP.GetChild(point).rotation, InHabitants);
                    enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

                    enemy.SetActive(true);


                    break;
                }
            }

            StartCoroutine(DelayEnemyPos(newP));

        }

        if (Input.GetKeyDown(KeyCode.G)) //Giant generate
        {

            var getVal = Random.Range(1,5);

            if (getVal == 1) newP = SpawnPoints;
            if (getVal == 2) newP = SpawnPoints2;
            if (getVal == 3) newP = SpawnPoints3;
            if (getVal == 4) newP = SpawnPoints4;



            var mesh = AIMesh.transform.GetChild(0).childCount - 1;
            var point = Random.Range(0, newP.childCount);
            var enemy = Instantiate(AIMesh, newP.GetChild(point).position, newP.GetChild(point).rotation, InHabitants);
            enemy.name = enemy.transform.GetChild(0).GetChild(mesh).name;
            var scaleMesh = enemy.transform.GetChild(0).gameObject;
            enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.1f);
            scaleMesh.transform.DOScale(Vector3.zero, 0);
            scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
            scaleMesh.transform.DOScale(Vector3.one, 0.25f);
            scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);

            enemy.SetActive(true);

            Shake.gameObject.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.Q)) //Dragon2
        {
            Dragon2.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (!BoneShield.isPlaying)
            {
                BoneShield.Play();

                Shake.gameObject.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(!Bird.transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
            {
                Bird.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                Bird.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(!Trap.isPlaying)
            {
                Trap.Play();

                Shake.gameObject.SetActive(true);

            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            TornadoMachine.SetTrigger("Generate");

            var ey = Instantiate(Tornado, Tornado.transform.position, Tornado.transform.rotation, InHabitants);
            ey.SetActive(true);
            StartCoroutine(TornadoDelay(ey.gameObject));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!Lava.isPlaying)
            {
                Lava.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) //DefendArmy
        {
            foreach(Transform child in FirstArmyControl)
            {
                if(child.name != "Reserved")
                {



                    var mesh = Random.Range(0, OurArmyMesh.transform.GetChild(0).childCount-2);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos1.position, SpawnMachinePos1.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    SpawnMachineAnim.SetTrigger("Generate");
                    connectionEconomy.CalculateMoney(-100);

                    enemy.SetActive(true);


                    break;

                }
            }
        }


        if (Input.GetKeyDown(KeyCode.U)) //DefendArmy
        {
            foreach (Transform child in FirstArmyControl)
            {
                if (child.name != "Reserved")
                {
                    var mesh = Random.Range(1, OurArmyMesh.transform.GetChild(0).childCount-1);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos2.position, SpawnMachinePos2.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 0.25f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.GetComponent<ArmyMovement>().İsRobot = true;
                    enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                    enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;

                    scaleMesh.transform.DOScale(Vector3.one, 0.25f);
                    scaleMesh.transform.DOShakeScale(0.5f, 0.5f, 2).SetDelay(0.25f);
                    SpawnMachineAnim2.SetTrigger("Generate");
                    connectionEconomy.CalculateMoney(-100);

                    enemy.SetActive(true);


                    break;

                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Y)) //DefendArmy
        {
            foreach (Transform child in FirstArmyControl)
            {
                if (child.name != "Reserved")
                {
                    var mesh = Random.Range(2, OurArmyMesh.transform.GetChild(0).childCount);
                    var point = Random.Range(0, OurArmySpawnPoints.childCount);
                    var enemy = Instantiate(OurArmyMesh, SpawnMachinePos3.position, SpawnMachinePos3.rotation, InHabitants);
                    var scaleMesh = enemy.transform.GetChild(0).gameObject;
                    enemy.transform.GetChild(0).DOLocalMoveY(0.408f, 2).SetDelay(2.5f);
                    scaleMesh.transform.DOScale(Vector3.zero, 0);
                    scaleMesh.transform.GetChild(mesh).gameObject.SetActive(true);
                    enemy.GetComponent<ArmyMovement>().İsRobot = true;
                    enemy.GetComponent<ArmyMovement>().RobotAnim = scaleMesh.transform.GetChild(mesh).gameObject.GetComponent<Animator>();
                    enemy.name = scaleMesh.transform.GetChild(mesh).gameObject.name;
                    scaleMesh.transform.DOScale(Vector3.one, 1).SetDelay(2);
                    SpawnMachineAnim3.SetTrigger("Generate");
                    connectionEconomy.CalculateMoney(-100);
                    enemy.SetActive(true);


                    break;

                }
            }
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Dragon.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.P)) //Power
        {
           
            for(int i= 0; i<= Gun.BatteryDisc.Length - 1; i++)
            {
                if (!Gun.BatteryDisc[i].gameObject.activeSelf)
                {
                    Gun.BatteryDisc[i].gameObject.SetActive(true);
                    Gun.batteryValue = Mathf.Clamp(Gun.batteryValue += 1,0,7);
                    Gun.FindNewEnemy();
                    connectionEconomy.CalculateMoney(-1*PowerFee);
                    Haptic.MediumTaptic();
                    break;
                }
            }
        }
    }
}
