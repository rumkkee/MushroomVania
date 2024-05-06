using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpore : Spore
{
    public delegate void TeleportSporeCollided(Vector3 collisionPoint);
    public static event TeleportSporeCollided OnTeleportSporeCollided;

    protected override void SporeCollisionEvents()
    {
        base.SporeCollisionEvents();
        OnTeleportSporeCollided(transform.position);
        ThrowTest.instance.StartCooldown();
    }
}
