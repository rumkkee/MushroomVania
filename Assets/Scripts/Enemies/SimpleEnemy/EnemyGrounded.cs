using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrounded : MonoBehaviour
{
    public SimpleEnemyMovement enemy;

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Sword" && other.gameObject.tag == "Enemy")
        {
            return; //Checks if the enemy is going to fall of the ground if it keeps going.
        }
        enemy.Turn();
    }
}
