using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    public Movement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player == null)
        {
            Debug.Log("Ceiling Collision Occurred");
            playerMovement.OnCeilingCollision();
        }
    }
}
