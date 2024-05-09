using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiedPlayerDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player != null)
        {
            StackEnemy stackEnemy = GetComponent<StackEnemy>();
            stackEnemy.playerDetected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited");
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player != null)
        {
            StackEnemy stackEnemy = GetComponent<StackEnemy>();
            stackEnemy.playerOutOfRange();
        }
    }
}
