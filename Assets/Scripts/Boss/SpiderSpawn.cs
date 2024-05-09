using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;
    public GameObject roomPrefab;

    private void Start()
    {
        bossAttack.SpiderBall += Attack;
    }

    void OnDestroy(){
        bossAttack.SpiderBall -= Attack;
    }

    public void Attack()
    {
        Debug.Log("SpawnSpider starts");
        SpawnSpider();
    }

    public void SpawnSpider(){
        GameObject spider = Instantiate(spiderPrefab, transform.position, Quaternion.identity);
        spider.transform.SetParent(roomPrefab.transform);
    }
}
