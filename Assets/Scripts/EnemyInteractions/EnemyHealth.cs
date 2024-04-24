using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float swordDamage = 50f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            health -= swordDamage;
            if(health <= 0){
                Destroy(gameObject); //Health of enemy and destroy if below or equal to 0
            }
        }
    }
}
