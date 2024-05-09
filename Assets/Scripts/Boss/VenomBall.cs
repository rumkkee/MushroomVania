using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBall : MonoBehaviour
{
    [SerializeField] private float timeBeforeSelfDestruct = 6f;

    private void Start()
    {
        Destroy(this.gameObject, timeBeforeSelfDestruct);
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
