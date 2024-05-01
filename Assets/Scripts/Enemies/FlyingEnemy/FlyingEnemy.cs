using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private FlyingEnemyMovement movement;

    private void Awake()
    {
        movement = GetComponent<FlyingEnemyMovement>();
    }

    public void OnPlayerEntersRadius(Transform playerTransform)
    {
        movement.ChangeState(movement.FollowPlayer(playerTransform));
    }

    public void OnPlayerExitsRadius()
    {
        movement.ChangeState(movement.IdleRoam());
    }
}
