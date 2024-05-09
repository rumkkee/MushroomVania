using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;

    private void Start()
    {
        bossAttack.SpiderBall += Attack;
    }

    public void Attack()
    {
        Debug.Log("SpawnSpider starts");
        SpawnSpider();
    }

    public void SpawnSpider(){
        Instantiate(spiderPrefab, transform.position, Quaternion.identity);
    }
}
