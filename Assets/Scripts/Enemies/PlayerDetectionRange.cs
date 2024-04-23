using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionRange : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player != null)
        {
            FlyingEnemy enemy = GetComponentInParent<FlyingEnemy>();
            enemy?.OnPlayerEntersRadius();
        }
    }
}
