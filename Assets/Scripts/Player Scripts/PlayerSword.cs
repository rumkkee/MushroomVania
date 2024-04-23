using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);

            // Handling knockback
            Vector2 knockbackDirection = enemy.transform.position - this.transform.position;

            EnemyMovement enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyMovement?.TakeKnockback(knockbackDirection);
        }
    }
}
