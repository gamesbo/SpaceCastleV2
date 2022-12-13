using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    bool oneShot;
    [SerializeField] ProgressBarPro Bar;
    [SerializeField] CardSpawnEvent CoroutineStopper;
    [SerializeField] GameObject InHabitants;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ShieldAttackEffect>() && other.gameObject.GetComponent<ShieldAttackEffect>().damage > 0 && other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {

            Bar.Value -= 0.0001f;

            if(Bar.Value <= 0)
            {
                if(!oneShot)
                {
                    CoroutineStopper.StopCoroutine(CoroutineStopper.GenerateEnemy());
                    EKTemplate.LevelManager.instance.Fail();
                    Haptic.NotificationErrorTaptic();
                    InHabitants.tag = "GameOver";
                    oneShot = true;
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("CastleHealth", Bar.Value);
        PlayerPrefs.SetInt("NotFirstGP", 1);

    }
}
