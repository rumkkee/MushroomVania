using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private SlimeEnemyMovement movement;//This script just sends the data from the Attack and Follow frog scripts into the movement.

    private void Awake()
    {
        movement = GetComponent<SlimeEnemyMovement>();
    }

    public void OnPlayerEntersRadius(Transform playerTransform)
    {
        movement.ChangeTarget(playerTransform);
    }

    public void OnPlayerExitsRadius()
    {
        movement.ChangeState();
    }
}
