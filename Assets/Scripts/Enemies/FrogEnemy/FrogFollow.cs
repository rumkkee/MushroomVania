using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFollow : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>(); //These functions get the position of the players and passes it into the frog movement
            enemy?.OnPlayerEntersRadius(player.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>();
            enemy?.OnPlayerExitsRadius();
        }
    }
}
