using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            SpiderEnemy enemy = GetComponentInParent<SpiderEnemy>();//These functions get the position of the players and passes it into the frog movement
            enemy?.OnPlayerEntersBox();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            SpiderEnemy enemy = GetComponentInParent<SpiderEnemy>();
            enemy?.OnPlayerExitsBox();
        }
    }
}

