using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject[] rooms; //this array holds references to all room prefabs
    public GameObject player; 
    private GameObject currentRoom; //this is the current active room 
    private CharacterController playerCont; //player's character controller which will be deactivated then reactivated
    
    
    // Start is called before the first frame update
    void Start()
    {
        //get access to the player's character controller  
        playerCont = player.GetComponent<CharacterController>(); 
        
        if (playerCont != null)
        {
            playerCont.enabled = true; // if exists, make sure it starts out as enabled 
        }
        else
        {
            Debug.LogError("CharacterController component not found on playerObject.");
            return;
        }
        
        
        if (rooms.Length > 0)
        {
            currentRoom = Instantiate(rooms[0], Vector3.zero, Quaternion.identity); 
            //this activates the starting room, which should be 0 in the array. 
        }
        else
        {
            Debug.Log("no room prefabs in the rooms array!");
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //this method will be called when a player enters a trigger
    //specify the index in the specific trigger/door itself 
    public void TransitionToRoomLogic(int roomIndex, string exitName )
    {
        Debug.Log("Entering Room"+ roomIndex + "through door " + exitName);
        //transition logic 
        
        //deactivate current room if it already exists 
        if (currentRoom != null)
        {
            Debug.Log("destroying current room");
            Destroy(currentRoom);
        }
        
        //check to see if the requested room index is within the bounds of the array
        if (roomIndex >= 0 && roomIndex < rooms.Length)
        {
            //instantiate new room, set to current room, set proper player position 
            currentRoom = Instantiate(rooms[roomIndex], Vector3.zero, Quaternion.identity); 
            Debug.Log("creating room" + roomIndex);
        }
        else
        {
            Debug.Log("Sorry, bud. You don't have a valid index there.");
        }
        
        //room instantiated, now spawn player at exit door:

        //find the object whose position we will spawn at within the new room
        GameObject destinationObject = GameObject.Find(exitName);
        Debug.Log("destination object is" + destinationObject);
        if (destinationObject != null)
        {
            playerCont.enabled = false;
            Vector3 playerSpawnPosition = destinationObject.transform.position;
            player.transform.position = playerSpawnPosition; 
            Debug.Log("spawning player at " + playerSpawnPosition);
            playerCont.enabled = true;
        }
        else
        {
            Debug.LogError("Sorry bud, your destination object not found in the new room!");
        }
        




    }

    
}
