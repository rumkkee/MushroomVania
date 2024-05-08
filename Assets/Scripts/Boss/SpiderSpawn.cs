using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("SpawnSpider starts");
            SpawnSpider();
        }
    }

    public void SpawnSpider(){
        Instantiate(spiderPrefab, transform.position, Quaternion.identity);
    }
}
