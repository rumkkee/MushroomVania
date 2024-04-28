using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFollow : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>();
            enemy?.OnPlayerEntersRadius(player.transform);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            Movement player = other.gameObject.GetComponent<Movement>();
            FrogEnemy enemy = GetComponentInParent<FrogEnemy>();
            enemy?.OnPlayerExitsRadius();
        }
    }
}
