using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyGrounded : MonoBehaviour
{
    public SpiderEnemyMovement enemy;

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) { return; }
        if(other.gameObject.tag == "Sword" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Vines")
        {
            return; //Checks if the enemy is going to fall of the ground if it keeps going.
        }
        enemy.Turn();
    }
}
