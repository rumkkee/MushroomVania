using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void TreeCheckpoint(Vector3 checkpoint);
    public static event TreeCheckpoint PlayerCheckpoint; 
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            PlayerCheckpoint.Invoke(transform.position); //Puts the position of the checkpoint into the player for respawn
        }
    }
}
