using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemyMovement : EnemyMovement
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
    public GameObject tongueSwitch;
    public GameObject tongue;
    private Rigidbody rb;
    private Transform playerTarget;
    private bool canLick = false;
    private bool groundedLeft = false;
    private bool groundedRight = false;

    public AudioSource jumpSound;
    public AudioSource tongueSound;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(isGrounded || groundedLeft || groundedRight)
        {
            if(canLick && !isLicking)
            {
                isLicking = true;
                currentIdleTime = 0;//If the player is in range and grounded and is not currently in the lick process it will lick.
                LickPlayer();
                tongueSound.Play();
            } 
            else 
            {
                currentIdleTime += Time.deltaTime;
                if(currentIdleTime >= idleTime){
                    currentIdleTime = 0;//Has idle time for the frog between jumps.
                    if(isIdle)
                    {
                        IdleJump();//If player not range calls IdleJump if you are it calls FollowJump
                        jumpSound.Play();
                    } 
                    else 
                    {
                        FollowJump();
                        jumpSound.Play();
                    }
                }
            }
        }

        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.01f;//Just to detect if the frog is grounded or not.
        Vector3 leftStart = new Vector3(startPoint.x-col.bounds.extents.x, startPoint.y, startPoint.z);
        Vector3 rightStart = new Vector3(startPoint.x+col.bounds.extents.x, startPoint.y, startPoint.z);
        
        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            isGrounded = true;
        } 
        else 
        {
            isGrounded = false;
        }

        if (Physics.Raycast(leftStart, Vector3.down, out hit, halfHeight))
        {
            groundedLeft = true;
        } 
        else 
        {
            groundedLeft = false;
        }

        if (Physics.Raycast(rightStart, Vector3.down, out hit, halfHeight))
        {
            groundedRight = true;
        } 
        else 
        {
            groundedRight = false;
        }
    }

    public void LickPlayer()
    {
        Vector3 direction = playerTarget.position - transform.position;
        if(direction.x < 0)
        {
            tongueSwitch.transform.eulerAngles = new Vector3(0,0,0);//Gets the location of player to rotate where the tongue will spawn.
        } 
        else 
        {
            tongueSwitch.transform.eulerAngles = new Vector3(0,0,180);
        }
        StartCoroutine(LickedAlready());
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

    public void CanLick()
    {
        canLick = true;//Lets the frog be able to lick player
    }

    public void CannotLick()
    {
        canLick = false;//Frog cannot lick because player left area
    }

    public IEnumerator LickedAlready()
    {
        tongue.SetActive(true);
        yield return new WaitForSeconds(tongueTimer);//Sets the tongue active and then calls cooldown.
        tongue.SetActive(false);
        StartCoroutine(lickCooldown());
    }

    public IEnumerator lickCooldown()
    {
        yield return new WaitForSeconds(tongueCooldown);//Cooldown for licking
        isLicking = false;
    }
}
