using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(0,0,0);
    public delegate void roomRespawn();
    public static event roomRespawn onRoomRespawn; 
    void Start()
    {
        Checkpoint.PlayerCheckpoint += RecentCheckpoint;
        LivesManagement.onRespawn += Respawn;
        NewBehaviourScript.newRoomRespawn += RecentCheckpoint;
    }

    public void RecentCheckpoint(Vector3 checkpoint)
    {
        respawnPosition = checkpoint; //This gets the position of the last passed checkpoint
    }

    public void Respawn()
    {
        onRoomRespawn.Invoke();
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = false;
        transform.position = respawnPosition; //These lines of code have to disable the character controller to then move the player to the checkpoint
        controller.enabled = true;
    }
}
