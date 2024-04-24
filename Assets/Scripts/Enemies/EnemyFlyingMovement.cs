using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyFlyingMovement : EnemyMovement
{
    private Transform target;

    [SerializeField] private float chaseSpeed = 14f;
    [SerializeField] private float idleSpeed = 2f;

    private IEnumerator currentAction;

    private void Start()
    {
        ChangeState(IdleRoam());
    }

    public void ChangeState(IEnumerator enumerator)
    {
        if (currentAction != null)
        {
            StopCoroutine(currentAction);
        }
        currentAction = enumerator;
        StartCoroutine(currentAction);
    }

    public IEnumerator IdleRoam()
    {
        while (true)
        {
            // Choose a random direction to briefly roam towards
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);
            Vector2 randDirection = new Vector2(randX, randY).normalized;

            float moveDuration = Random.Range(0.5f, 3f);
            float remainingDuration = moveDuration;
            do
            {
                remainingDuration -= Time.fixedDeltaTime;
                rb.AddForce(randDirection * idleSpeed, ForceMode.Force);
                yield return new WaitForFixedUpdate();
            } while (remainingDuration > 0f);
        }    
    }

    public IEnumerator FollowPlayer(Transform playerTransform)
    {
        target = playerTransform;
        while (true)
        {
            Vector2 moveDirection = target.transform.position - transform.position;
            moveDirection = moveDirection.normalized;
            rb.AddForce(moveDirection * chaseSpeed, ForceMode.Force);
            yield return new WaitForFixedUpdate();
        }
    }
}
