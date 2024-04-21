using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations;

public class Movement : MonoBehaviour
{
    private float moveX, moveY, dashCD, coyoteCD, jumpBufferCD, useMaxFallSpeed;
    
    private bool isGrounded, isDashing, dashedInAir;
    
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
    [SerializeField] private Transform groundCheck;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        TeleportSpore.OnTeleportSporeCollided += Teleport;
    }

    void Update()
    {
        // Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, layerMask);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCD = jumpBuffer;
        }

        if (isGrounded)
        {
            coyoteCD = coyoteTime;
            moveY = -1f;
            dashedInAir = false;


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
    }

    private void Teleport(Vector3 targetPosition)
    {
        StartCoroutine(TeleportHelper(targetPosition, 0.1f));
    }

    IEnumerator TeleportHelper(Vector3 targetPosition, float duration)
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
}
