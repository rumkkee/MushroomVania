using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    private NewBehaviourScript roomManager;

    public AudioSource levelMusic;

    public AudioSource bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        //the name of the room management script is NewBehaviourScript
        roomManager = GetComponent<NewBehaviourScript>();

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

            if (!bossMusic.isPlaying)
            {
                bossMusic.Play();
            }
            
        }
        else
        {
            

            if (!levelMusic.isPlaying)
            {
                levelMusic.Play();
            }
        }
    }
}
