using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private EnemyFlyingMovement movement;

    private void Awake()
    {
        movement = GetComponent<EnemyFlyingMovement>();
    }

    public void OnPlayerEntersRadius()
    {
        movement.ChangeState(movement.FollowPlayer());
    }

    public void OnPlayerExitsRadius()
    {
        movement.ChangeState(movement.IdleRoam());
    }
}
