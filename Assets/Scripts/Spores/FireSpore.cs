using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpore : Spore
{
    [SerializeField] private int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeFireDamage(damage);

            // Handling knockback
            Vector2 knockbackDirection = enemy.transform.position - this.transform.position;

            EnemyMovement enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyMovement?.TakeKnockback(knockbackDirection);
        }


        if(other.gameObject.tag == "Vines"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
