using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParticlesOfTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem FireBase;
    [SerializeField] GameObject Garden;
    [SerializeField] Animator BurnSprite;
    bool oneShotGarden;

    private void OnParticleTrigger()
    {
        if (!FireBase.isPlaying) FireBase.Play();

        if (!oneShotGarden)
        {
            oneShotGarden = true;
            Garden.transform.DOScaleY(-1f, 20).SetDelay(5);
        }

        if (BurnSprite.GetCurrentAnimatorStateInfo(0).IsName("BurnIdle"))
        {
            BurnSprite.SetTrigger("Burn");
        }

    }
}
