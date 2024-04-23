using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowRange : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Movement player = other.gameObject.GetComponent<Movement>();
        if (player != null)
        {
            FlyingEnemy enemy = GetComponentInParent<FlyingEnemy>();
            enemy?.OnPlayerExitsRadius();
        }
    }
}
