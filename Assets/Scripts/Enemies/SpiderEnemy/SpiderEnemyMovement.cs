using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyMovement : EnemyMovement
{
    private Vector2 moveDirection = Vector2.right * 0.1f;
    public float speed = 10.0f;
    private Rigidbody rb;
    private bool turn = false;
    public GameObject webShooterRotate;
    public GameObject web;
    public GameObject webShootLocation;
    public float webCooldown = 3f;
    public float webSpeed = 20f;
    private float startRotation = 0;
    private bool canShoot = false;
    private bool isWebbing = false;
    private GameObject webbing;
    private Vector2 webDirection;
    
    void Update()
    {
        transform.position += new Vector3(moveDirection.x, 0, 0) * speed * Time.deltaTime; //Sends enemies in a direction
        if(webbing != null){
            webbing.transform.position += new Vector3(webDirection.x, 0, 0) * webSpeed * Time.deltaTime;
        }
        if(canShoot && !isWebbing){
            Shoot();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Webs")
        {
            return;
        }
        Turn();//if it collides it calls a function that reverse direction
    }

    public void Shoot(){
        webbing = Instantiate(web, webShootLocation.transform.position, Quaternion.identity);
        webDirection = moveDirection;
        StartCoroutine(WebCooldown());
    }

    public void Turn()
    {
        startRotation += 180;
        webShooterRotate.transform.eulerAngles = new Vector3(0, 0, startRotation);
        turn = !turn;
        moveDirection = new Vector2(-moveDirection.x, moveDirection.y); //reverses direction
    }

    public void CanShoot(){
        canShoot = true;
    }

    public void CannotShoot(){
        canShoot = false;
    }

    public IEnumerator WebCooldown()
    {
        isWebbing = true;
        yield return new WaitForSeconds(webCooldown);//Cooldown for licking
        isWebbing = false;
    }
}
