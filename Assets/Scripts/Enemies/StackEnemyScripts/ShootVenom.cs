using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVenom : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    [SerializeField] private float speed = 70f;
    [SerializeField] private string playerName = "Player (1)";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName);
        Vector3 dir = player.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(dir.x, 0f, 0f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Doesn't matter what we hit, just destroy the game object because
        //if it hit the player then the player script handles it
        Destroy(gameObject);
    }
}
