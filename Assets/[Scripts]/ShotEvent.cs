using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
    [SerializeField] int DamageDragon, DamageEnemyArmy, DamageOurArmy;

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Dragon"))
        {
            if (other.GetComponent<DragonsMovement>())
                other.GetComponent<DragonsMovement>().HealthDamage(DamageDragon);      
        }

        if (other.CompareTag("Live"))
        {
            if(other.GetComponent<EnemyMovement>() && other.GetComponent<EnemyMovement>().enabled)
            other.GetComponent<EnemyMovement>().HealthDamage(DamageEnemyArmy);

            if (other.GetComponent<ArmyMovement>() && other.GetComponent<ArmyMovement>().enabled)
                other.GetComponent<ArmyMovement>().HealthDamage(DamageOurArmy);
        }


    }
}
