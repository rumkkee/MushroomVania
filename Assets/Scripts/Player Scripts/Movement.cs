using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations;

public class Movement : MonoBehaviour
{
    public float speed,
        gravity,
        maxFallSpeed,
        jumpHeight,
        jumpBoost,
        dashSpeed,
        dashCooldown,
        dashDuration,
        dashMiniBoost,
        coyoteTime,
        jumpBuffer;

    [Range(0f, 1f)] public float glideEffectiveness;
    public LayerMask layerMask;

    private bool isGrounded, isDashing, teleport;

    private float moveX, moveY, dashCD, coyoteCD, jumpBufferCD, useMaxFallSpeed;
    private Vector2 dashDirection;
    private Transform groundCheck;
    private CharacterController controller;

    private Camera mainCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("Ground Check").transform;

        mainCamera = Camera.main;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, layerMask);


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));
            
            StartCoroutine(Teleport(targetPosition, 0.1f));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCD = jumpBuffer;
        }

        if (isGrounded)
        {
            coyoteCD = coyoteTime;
            moveY = -1f;


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
                else if (moveY < 0f)
                {
                    useMaxFallSpeed = maxFallSpeed * (1f - glideEffectiveness);
                }
            }
            else
            {
                useMaxFallSpeed = maxFallSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteCD >= 0f)
        {
            moveY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


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

    IEnumerator Teleport(Vector3 targetPosition, float duration)
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
