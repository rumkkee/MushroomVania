using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemyMovement : MonoBehaviour
{
    // public bool facingRight = false;
    public bool isGrounded = false;
    public bool isLicking = false;
    public bool isIdle = true;
    public float jumpForceX = 2f;
    public float jumpForceY = 4f;
    public float idleTime = 2f;
    public float currentIdleTime = 0f;
    public float tongueTimer = 1f;
    public float tongueCooldown = 2f;
    public GameObject tongue;
    private Rigidbody rb;
    private Transform playerTarget;
    private bool canLick = false;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(isGrounded){
            if(canLick && !isLicking){
                isLicking = true;
                currentIdleTime = 0;
                LickPlayer();
            } else {
                currentIdleTime += Time.deltaTime;
                if(currentIdleTime >= idleTime){
                    currentIdleTime = 0;
                    if(isIdle){
                        IdleJump();
                    } else {
                        FollowJump();
                    }
                }
            }
        }

        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.01f;
        
        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    public void LickPlayer(){
        Vector3 direction = playerTarget.position - transform.position;
        if(direction.x < 0){
            tongue.transform.eulerAngles = new Vector3(0,0,0);
        } else {
            tongue.transform.eulerAngles = new Vector3(0,0,180);
        }
        StartCoroutine(LickedAlready());
    }

    public void IdleJump(){
        int rand = Random.Range(1, 3);
        int direction = 0;
        if(rand == 1){
            direction = 1;
        } else {
            direction = -1;
        }
        rb.velocity = new Vector3(jumpForceX * direction, jumpForceY, 0);
    }

    public void FollowJump(){
        int jumpDirection = 0;
        Vector3 direction = playerTarget.position - transform.position;
        if(direction.x < 0){
            jumpDirection = -1;
        } else {
            jumpDirection = 1;
        }
        rb.velocity = new Vector3(jumpForceX * jumpDirection, jumpForceY, 0);
    }

    public void ChangeTarget(Transform player){
        isIdle = false;
        playerTarget = player;
    }

    public void ChangeState(){
        isIdle = false;
    }

    public void CanLick(){
        canLick = true;
    }

    public void CannotLick(){
        canLick = false;
    }

    public IEnumerator LickedAlready()
    {
        tongue.SetActive(true);
        yield return new WaitForSeconds(tongueTimer);
        tongue.SetActive(false);
        StartCoroutine(lickCooldown());
    }

    public IEnumerator lickCooldown(){
        yield return new WaitForSeconds(tongueCooldown);
        isLicking = false;
    }
}
