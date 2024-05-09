using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StackEnemy : MonoBehaviour
{
    public int length = 2;
    private Transform spawnPosition;
    private GameObject master;
    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public StackMasterHealth stackMasterHealth;
    private GameObject head;
    private Vector3 shootPos;
    public float fireRate = 0.5f;
    private float fireCountdown = 0f;
    public GameObject venomPrefab;
    private bool enemyActive = false;
    private bool isSurfaced = false;

    private void Awake()
    {
        master = this.gameObject;
        spawnPosition = this.transform;
        

        

        fireCountdown = 1f / fireRate;

        StartCoroutine(WaitThenMove());
    }

    private void Update()
    {
        if (enemyActive && !isSurfaced)
        {
            Surface();
            
        }

        if (!enemyActive && isSurfaced)
        {
            Burrow();
        }

        if (fireCountdown <= 0f && enemyActive)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void SpawnBody(int offsetNum)
    {
        Instantiate(bodyPrefab, spawnPosition.position + (Vector3.up * offsetNum), Quaternion.identity, master.transform);
    }

    void SpawnHead(int offsetNum)
    {
        head = Instantiate(headPrefab, spawnPosition.position + (Vector3.up * offsetNum), Quaternion.identity, master.transform);
    }

    public void decreaseLength()
    {
        length--;
    }

    public void headDestroyed()
    {
        stackMasterHealth.HeadDestroyed();
    }

    public void bodyDestroyed()
    {
        decreaseLength();
        Move();
    }

    private void Surface()
    {
        Debug.Log("Surfacing");
        for (int i = 0; i < length - 1; i++)
        {
            SpawnBody(i);
        }

        SpawnHead(length-1);
        isSurfaced = true;
        
        //update the shoot position after resurfacing
        shootPos = head.transform.position;
        shootPos += Vector3.left * 3f;
    }

    public void Burrow()
    {
        for (int i = 0; i < length; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        isSurfaced = false;
    }

    private void Move()
    {
        Burrow();
        transform.Translate(Vector3.left * 3);
        StartCoroutine(WaitThenSurface());
    }

    private void Shoot()
    {
        Instantiate(venomPrefab, shootPos, quaternion.identity);
    }

    public void playerDetected()
    {
        enemyActive = true;
        Debug.Log("isActive: " + enemyActive);
    }

    public void playerOutOfRange()
    {
        enemyActive = false;
    }

    IEnumerator WaitThenMove()
    {
        yield return new WaitForSecondsRealtime(2f);
        Move();
    }

    IEnumerator WaitThenSurface()
    {
        yield return new WaitForSecondsRealtime(2f);
        Surface();
    }
}
