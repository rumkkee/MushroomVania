using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBall : MonoBehaviour
{
    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }
}
