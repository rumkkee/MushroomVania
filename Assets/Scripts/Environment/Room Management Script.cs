using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject[] rooms; //this array holds references to all room prefabs
    private GameObject currentRoom; //this is the current active room 
    
    
    // Start is called before the first frame update
    void Start()
    {
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
    public void TransitionToRoomLogic(int roomIndex, Vector3 playerSpawnPosition )
    {
        //deactivate current room if it already exists 
        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }
        
        
        //check to see if the requested room index is within the bounds of the array
        if (roomIndex >= 0 && roomIndex < rooms.Length)
        {
            //instantiate new room, set to current room, set propwer player position 
            currentRoom = Instantiate(rooms[roomIndex], Vector3.zero, Quaternion.identity); 
            MovePlayerToSpawnPosition(playerSpawnPosition);
        }
        else
        {
            Debug.Log("Sorry, bud. You don't have a valid index there.");
        }
        
    }


    private void MovePlayerToSpawnPosition(Vector3 spawnPosition)
    {
        
    }
    
    
}
