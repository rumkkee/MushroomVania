using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    private SpiderEnemyMovement movement;//This script just sends the data from the Attack and Follow frog scripts into the movement.

    private void Awake()
    {
        movement = GetComponent<SpiderEnemyMovement>();
    }
    public void OnPlayerEntersBox()
    {
        movement.CanShoot();
    }

    public void OnPlayerExitsBox()
    {
        movement.CannotShoot();
    }
}
