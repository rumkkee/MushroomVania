using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveX, moveY, dashCD, coyoteCD, jumpBufferCD, useMaxFallSpeed;
    
    private bool isGrounded, isDashing, dashedInAir, jumpApexReached = false,
        facingRight = true;
    
    public LayerMask layerMask;
    private float maxFallSpeed = -35f,
        speed = 10f,
        dashSpeed = 35f,
        dashDuration = 0.15f,
        coyoteTime = 0.05f,
        dashCooldown = 0.25f,
        gravity = -90f,
        jumpHeight = 2,
        jumpBuffer = 0.1f,
        dashMiniBoost = 10,
        jumpBoost = 30,
        glideEffectiveness = 0.9f;
    
    private Vector2 dashDirection;
    private Transform groundCheck;
    private CharacterController controller;

    //New set up specific to wall jump
    private bool canWallJump = false;
    private bool isWallJumping = false;
    private Vector3 wallNormal;
    private float wallJumpCD = 0f;
    //wallJumpMultiplier must stay negative
    public float wallJumpMultiplier = 1f;
    //wallJumpDistance should stay positive
    public float wallJumpDistance = 5.0f;
    
    //PlayerSounds audio sources
    public AudioSource walk;
    public AudioSource jump;
    public AudioSource dash;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("Ground Check").transform;
        TeleportSpore.OnTeleportSporeCollided += Teleport;
    }

    void Update()
    {
        // Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, layerMask);
        
        if (Input.GetKeyDown(KeyCode.Space) && canWallJump)
        {
            if (canWallJump)
            {
                // Perform wall jump
                Jump(wallNormal);
                canWallJump = false; // Reset wall jump ability
            }
            else
            {
                Jump(Vector3.up);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCD = jumpBuffer;
        }
        if (isGrounded)
        {
            coyoteCD = coyoteTime;
            moveY = -1f;
            dashedInAir = false;

            // Checks for wall jump
            isWallJumping = false;
            // resets the countdown when you land so there is no horizontal movement to the next jump
            wallJumpCD = 0f;

            if (jumpBufferCD > 0f)
            {
                moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && !isDashing)
            {
                if (moveY >= 0f)
                {
                    moveY += jumpBoost * Time.deltaTime;
                }
            }
            else
            {
                useMaxFallSpeed = maxFallSpeed;
            }

            if (Input.GetKey(KeyCode.W) && !isDashing)
            {
                if (moveY < 0f)
                {
                    useMaxFallSpeed = maxFallSpeed * (1f - glideEffectiveness);
                }
            }
        }
        
        // Glide
        if (Input.GetKeyDown(KeyCode.Space) && coyoteCD >= 0f)
        {
            moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Gravity
        if (!isDashing)
        {
            controller.Move(Vector2.up * moveY * Time.deltaTime);
            if (moveY >= useMaxFallSpeed)
            {
                moveY += gravity * Time.deltaTime;
            }
            else
            {
                moveY = useMaxFallSpeed;
            }
        }
        else
        {
            if (moveY < 0f)
            {
                moveY = dashMiniBoost;
            }
        }
        
        // Dash
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0f && !dashedInAir)
        {
            isDashing = true;
            dashCD = dashCooldown;
            dashDirection = (Vector2.right * moveX) / Mathf.Abs(moveX);
            dashedInAir = true;
            dash.Play();
        }
        if (isDashing && dashCD >= dashCooldown - dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(Vector2.right * moveX * speed * Time.deltaTime);
        }
        if (dashCD <= dashCooldown - dashDuration)
        {
            isDashing = false;
        }
        if (dashCD > 0f)
        {
            dashCD -= Time.deltaTime;
        }
        if (coyoteCD > 0f)
        {
            coyoteCD -= Time.deltaTime;
        }
        if (jumpBufferCD > 0f)
        {
            jumpBufferCD -= Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (!walk.isPlaying)
            {
                walk.Play();
            }
        }
        else
        {
            walk.Stop();
        }
    }
    
    public void OnCeilingCollision()
    {
        moveY = 0f;
        jumpApexReached = true;
    }
    
    private void Jump(Vector3 jumpDirection)
    {
        // Get the horizontal input direction from player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 horizontalJumpDirection = new Vector3(horizontalInput, 0f, 0f).normalized;

        // Calculate the final jump direction by combining the input direction and wall normal
        Vector3 finalJumpDirection = (horizontalJumpDirection + jumpDirection).normalized;

        // Apply jump forces
        moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        moveX = finalJumpDirection.x * Mathf.Sqrt(jumpHeight * wallJumpMultiplier * gravity);
        jump.Play();
    }

    private void Teleport(Vector3 targetPosition)
    {
        StartCoroutine(TeleportHelper(targetPosition, 0.1f));
    }

    private IEnumerator TeleportHelper(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPosition;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isGrounded && hit.collider.CompareTag("Wall"))
        {
            // Store the wall normal for wall jumping
            wallNormal = hit.normal.normalized;
            canWallJump = true;
        }
    }
}
