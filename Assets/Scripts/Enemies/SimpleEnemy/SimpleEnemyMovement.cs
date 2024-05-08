using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : EnemyMovement
{
    private Vector2 moveDirection = Vector2.right * 0.1f;
    public float speed = 10.0f;
    private Rigidbody rb;
    private bool turn = false;
    private SpriteRenderer spider;
    void Start(){
        spider = GetComponent<SpriteRenderer>();
        spider.flipX = !turn;
        int rand = Random.Range(1, 3);
        if(rand == 1){
            Turn();
        }
        
    }
    void Update()
    {
        transform.position += new Vector3(moveDirection.x, 0, 0) * speed * Time.deltaTime; //Sends enemies in a direction
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            return;
        }
        Turn();//if it collides it calls a function that reverse direction
    }

    public void Turn()
    {
        spider.flipX = turn;
        turn = !turn;
        moveDirection = new Vector2(-moveDirection.x, moveDirection.y); //reverses direction
    }

}
