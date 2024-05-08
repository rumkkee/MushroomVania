using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : EnemyMovement
{
    public bool isGrounded = false;
    public bool isIdle = true;
    public float jumpForceX = 2f;
    public float jumpForceY = 4f;
    public float idleTime = 2f;
    public float currentIdleTime = 0f;
    private Rigidbody rb;
    private Transform playerTarget;
    private bool groundedLeft = false;
    private bool groundedRight = false;
    private Animator ar;
    private bool landedAlready = false;

    
    void Start()
    {
        ar = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(isGrounded || groundedLeft || groundedRight)
        {
            currentIdleTime += Time.deltaTime;
            if(currentIdleTime >= idleTime){
                currentIdleTime = 0;//Has idle time for the frog between jumps.
                ar.SetTrigger("Jump");
                if(isIdle)
                {
                    IdleJump();//If player not range calls IdleJump if you are it calls FollowJump
                } 
                else 
                {
                    FollowJump();
                }
            }
        }

        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.1f;//Just to detect if the frog is grounded or not.
        Vector3 leftStart = new Vector3(startPoint.x-col.bounds.extents.x, startPoint.y, startPoint.z);
        Vector3 rightStart = new Vector3(startPoint.x+col.bounds.extents.x, startPoint.y, startPoint.z);
        
        RaycastHit hit;
        Debug.DrawRay(rightStart, Vector3.down * halfHeight, Color.green);
        Debug.DrawRay(leftStart, Vector3.down * halfHeight, Color.green);
        Debug.DrawRay(startPoint, Vector3.down * halfHeight, Color.green);
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            isGrounded = true;
            if(!landedAlready){
                ar.SetTrigger("Land");
                landedAlready = true;
            }
        } 
        else 
        {
            isGrounded = false;
            if(landedAlready){
                landedAlready = false;
            }
        }

        if (Physics.Raycast(leftStart, Vector3.down, out hit, halfHeight))
        {
            if(!landedAlready){
                ar.SetTrigger("Land");
                landedAlready = true;
            }
            groundedLeft = true;
        } 
        else 
        {
            groundedLeft = false;
            if(landedAlready){
                landedAlready = false;
            }
        }

        if (Physics.Raycast(rightStart, Vector3.down, out hit, halfHeight))
        {
            if(!landedAlready){
                ar.SetTrigger("Land");
                landedAlready = true;
            }
            groundedRight = true;
        } 
        else 
        {
            groundedRight = false;
            if(landedAlready){
                landedAlready = false;
            }
        }
    }

    public void IdleJump()
    {
        int rand = Random.Range(1, 3);
        int direction = 0;
        if(rand == 1)
        {
            direction = 1;//Jumps the frog in a random direction when called.
        }
        else 
        {
            direction = -1;
        }
        rb.velocity = new Vector3(jumpForceX * direction, jumpForceY, 0);
    }

    public void FollowJump()
    {
        int jumpDirection = 0;
        Vector3 direction = playerTarget.position - transform.position;
        if(direction.x < 0)
        {
            jumpDirection = -1;//Jumps the frog in the direction of the player
        } 
        else 
        {
            jumpDirection = 1;
        }
        rb.velocity = new Vector3(jumpForceX * jumpDirection, jumpForceY, 0);
    }

    public void ChangeTarget(Transform player)
    {
        isIdle = false;
        playerTarget = player;//Sets player to the target and changes it so it follows the player.
    }

    public void ChangeState()
    {
        isIdle = false;//Sets it back to random jumping
    }
}
