using UnityEngine;
using System.Collections;

public class AntiPlayerRespawn : MonoBehaviour
{
    public GameObject antiPlayerPrefab; // Reference to the antiplayer prefab
    private Vector3 respawnOffset = new Vector3(-6.5f, 4.5f, 0); // Offset for respawning the antiplayer

    private Vector3 respawnPosition = Vector3.zero;

    void Start()
    {
        Checkpoint.PlayerCheckpoint += RecentCheckpoint;
        LivesManagement.onRespawn += Respawn;
    }

    public void RecentCheckpoint(Vector3 checkpoint)
    {
        respawnPosition = checkpoint; // Get the position of the last passed checkpoint
    }

    public void Respawn()
    {
        // Move the original instance of the antiplayer to the respawn position with the offset
        transform.position = respawnPosition + respawnOffset;
    }

    public void OnDestroy()
    {
        Checkpoint.PlayerCheckpoint -= RecentCheckpoint;
        LivesManagement.onRespawn -= Respawn;
    }
}