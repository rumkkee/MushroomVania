using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    private NewBehaviourScript roomManager;

    public AudioSource levelMusic;

    public AudioSource bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentRoom = roomManager.getCurrentRoom();
        if (currentRoom.name == "Room 6 - boss(Clone)")
        {
            //in the boss room
            if (levelMusic.isPlaying)
            {
                levelMusic.Stop();
            }
            bossMusic.Play();
        }
        else
        {
            //in normal rooms
            if (bossMusic.isPlaying)
            {
                //make sure that the boss music is not playing
                bossMusic.Stop();
            }

            if (!levelMusic.isPlaying)
            {
                levelMusic.Play();
            }
        }
    }
}
