using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    private FrogEnemyMovement movement;//This script just sends the data from the Attack and Follow frog scripts into the movement.

    private void Awake()
    {
        movement = GetComponent<FrogEnemyMovement>();
    }

    public void OnPlayerEntersRadius(Transform playerTransform)
    {
        movement.ChangeTarget(playerTransform);
    }

    public void OnPlayerExitsRadius()
    {
        movement.ChangeState();
    }

    public void OnPlayerEntersBox()
    {
        movement.CanLick();
    }

    public void OnPlayerExitsBox()
    {
        movement.CannotLick();
    }
}
