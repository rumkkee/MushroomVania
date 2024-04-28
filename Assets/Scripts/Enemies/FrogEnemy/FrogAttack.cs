using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>();//These functions get the position of the players and passes it into the frog movement
            enemy?.OnPlayerEntersBox();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>();
            enemy?.OnPlayerExitsBox();
        }
    }
}
