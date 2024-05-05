using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordycepSpore : Spore
{
    public delegate void CordycepsSporeCollided(Vector3 collisionPoint);
    public static event CordycepsSporeCollided OnCordycepsSporeCollided;

    protected override void SporeCollisionEvents()
    {
        base.SporeCollisionEvents();
        OnCordycepsSporeCollided(transform.position);
    }
}