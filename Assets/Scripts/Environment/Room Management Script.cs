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
    public GameObject mainCam;
    private Camera currentCam;
    private int currentRoomRespawn;

    public delegate void newRoom(Vector3 position);
    public static event newRoom newRoomRespawn; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        //get access to the player's character controller  
        playerCont = player.GetComponent<CharacterController>();
        currentCam = mainCam.GetComponent<Camera>(); 
        //make sure player controller is enabled if it is not 
        if (playerCont != null)
        {
            playerCont.enabled = true; // if exists, make sure it starts out as enabled 
        }
        else
        {
            Debug.LogError("CharacterController component not found on playerObject.");
            return;
        }
        
        //instantiate first room
        if (rooms.Length > 0)
        {
            currentRoom = Instantiate(rooms[0], Vector3.zero, Quaternion.identity); 
            currentRoomRespawn = 0;
            //this activates the starting room, which should be 0 in the array. 
        }
        else
        {
            Debug.Log("no room prefabs in the rooms array!");
        }
        
        PlayerRespawn.onRoomRespawn += RespawnRoom;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCamera()
    {
        return mainCam; 
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
            currentRoomRespawn = roomIndex;
            Debug.Log("creating room" + roomIndex);
            
            //currentRoom.GetComponent<>(); 
            //GameObject childObject = prefabInstance.transform.Find("ChildName").gameObject;
            
        }
        else
        {
            Debug.Log("Sorry, bud. You don't have a valid index there.");
        }
        
        //room instantiated, now spawn player at exit door:

        //find the object whose position we will spawn at within the new room
        GameObject destinationObject = GameObject.Find(exitName);
        Debug.Log("destination object is" + destinationObject);
        if (destinationObject != null) //make sure destination object exists
        {
            playerCont.enabled = false; //disable player controller 
            Vector3 playerSpawnPosition = destinationObject.transform.position; //same position as destination object
            player.transform.position = playerSpawnPosition; 
            Debug.Log("spawning player at " + playerSpawnPosition);
            newRoomRespawn.Invoke(playerSpawnPosition);
            playerCont.enabled = true; //reenable 
        }
        else
        {
            Debug.LogError("Sorry bud, your destination object not found in the new room!");
        }
        
        //attach camera object to backgrounds 
        



    }
    
    
    //New function to be able to play boss music only in boss room
    public GameObject getCurrentRoom()
    {
        return currentRoom;
    }

    public void RespawnRoom(){
        if(currentRoom != null)
        {
            Destroy(currentRoom);
        }
        currentRoom = Instantiate(rooms[currentRoomRespawn], Vector3.zero, Quaternion.identity);
        // currentRoom = rooms[currentRoomRespawn];
    }    
}
