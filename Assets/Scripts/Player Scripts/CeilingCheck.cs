using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    public Movement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ceiling Collision Occurred");
        playerMovement.OnCeilingCollision();
    }
}
