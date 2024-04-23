using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{

    public int nextRoomIndex;
    public NewBehaviourScript worldManager; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        worldManager = FindObjectOfType<NewBehaviourScript>();
        if (worldManager == null)
        {
            Debug.Log("sorry there bud, room manager not found!");
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            worldManager.TransitionToRoomLogic(nextRoomIndex, transform.position); 
            Debug.Log("entered door");
        }
    }
}
