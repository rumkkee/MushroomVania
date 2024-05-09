using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordycepSpore : Spore
{
    // public delegate void CordycepsSporeCollided(Vector3 collisionPoint);
    // public static event CordycepsSporeCollided OnCordycepsSporeCollided;

    // protected override void SporeCollisionEvents()
    // {
    //     base.SporeCollisionEvents();
    //     OnCordycepsSporeCollided(transform.position);
    // }

    [SerializeField] private int damage = 100;
    public GameObject ExplosionRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){return;}
        if(other.isTrigger){return;}
        Destroy(gameObject);
        Instantiate(ExplosionRadius, transform.position, Quaternion.identity);
    }
}