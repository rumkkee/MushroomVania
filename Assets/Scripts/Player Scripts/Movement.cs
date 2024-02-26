using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed, gravity, maxFallSpeed, jumpHeight, jumpBoost, dashSpeed, dashCooldown, dashDuration, dashMiniBoost, coyoteTime, jumpBuffer;
    public LayerMask layerMask;

    private bool isGrounded, isDashing;
    private float moveX, moveY, dashCD, coyoteCD, jumpBufferCD;
    private Vector2 dashDirection;
    private Transform groundCheck;
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("Ground Check").transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, layerMask);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCD = jumpBuffer;
        }

        if (isGrounded)
        {
            coyoteCD = coyoteTime;
            moveY = -1f;

            if(jumpBufferCD > 0f)
            {
                moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && moveY >= 0f && !isDashing)
            {
                moveY += jumpBoost * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteCD >= 0f)
        {
            moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }



        if (!isDashing)
        {
            controller.Move(Vector2.up * moveY * Time.deltaTime);
            
            if(moveY >= maxFallSpeed)
            {
                moveY += gravity * Time.deltaTime;
            }
        }
        else
        {
            if (moveY < 0f)
            {
                moveY = dashMiniBoost;
            }
        }






        moveX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0f)
        {
            isDashing = true;
            dashCD = dashCooldown;
            dashDirection = (Vector2.right * moveX) / Mathf.Abs(moveX);
        }

        if (isDashing && dashCD >= dashCooldown - dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(Vector2.right * moveX * speed * Time.deltaTime);
        }

        if(dashCD <= dashCooldown - dashDuration)
        {
            isDashing = false;
        }

        if(dashCD > 0f)
        {
            dashCD -= Time.deltaTime;
        }

        if(coyoteCD > 0f)
        {
            coyoteCD -= Time.deltaTime;
        }

        if (jumpBufferCD > 0f)
        {
            jumpBufferCD -= Time.deltaTime;
        }
    }
}
