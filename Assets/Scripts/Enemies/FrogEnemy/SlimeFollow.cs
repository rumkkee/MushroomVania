using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFollow : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            SlimeEnemy enemy = GetComponentInParent<SlimeEnemy>(); //These functions get the position of the players and passes it into the frog movement
            enemy?.OnPlayerEntersRadius(player.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            SlimeEnemy enemy = GetComponentInParent<SlimeEnemy>();
            enemy?.OnPlayerExitsRadius();
        }
    }
}
