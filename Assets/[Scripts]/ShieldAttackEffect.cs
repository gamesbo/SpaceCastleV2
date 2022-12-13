using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldAttackEffect : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hex"))
        {
            other.tag = "Untagged";
            ColorChangeEffect(other.gameObject);
            DamageEffect(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hex") && this.gameObject.CompareTag("ShotObject"))
        {
            collision.gameObject.tag = "Untagged";
            ColorChangeEffect(collision.gameObject);
            DamageEffect(collision.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Hex"))
        {
            other.tag = "Untagged";
            ColorChangeEffect(other.gameObject);
            DamageEffect(other.gameObject);
        }
    }

    void ColorChangeEffect(GameObject other)
    {
        Material mat = other.GetComponent<MeshRenderer>().material;
        var firstColor = mat.color;
        mat.DOColor(Color.cyan, 0.5f).From();
    }

    void DamageEffect(GameObject other)
    {
        other.transform.DOShakePosition(0.5f, 0.25f, 3).OnComplete(() => other.tag = "Hex");

        if (name != "Effect")
        {
            var getHexHealth = int.Parse(other.gameObject.name);
            getHexHealth -= damage;
            other.name = (getHexHealth).ToString();

            if(getHexHealth <= 0)
            {

                other.gameObject.SetActive(false);


                if(!other.transform.parent.parent.GetChild(1).GetComponent<ParticleSystem>().isPlaying)
                other.transform.parent.parent.GetChild(1).GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
