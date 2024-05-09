using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVenom : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 70f;
    [SerializeField] private string playerName = "Player (1)";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            //PlayerHit
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
