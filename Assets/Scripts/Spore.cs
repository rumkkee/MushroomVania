using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{

    public delegate void SporeCollided(Vector3 collisionPoint);
    public static event SporeCollided OnTeleportSporeCollided;

    public static Spore instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddImpulse(Vector2 direction, float speed)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player == null)
        {
            //Debug.Log("Create Behavior Here!");
            OnTeleportSporeCollided(transform.position);
            Destroy(this.gameObject);
        }  
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
