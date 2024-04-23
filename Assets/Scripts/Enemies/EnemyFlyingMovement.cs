using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{
    public GameObject target;
    private Rigidbody rb;

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {  
        //StartCoroutine(FollowPlayer());  
    }

    public IEnumerator FollowPlayer()
    {
        while (true)
        {
            Vector2 moveDirection = target.transform.position - transform.position;
            rb.AddForce(moveDirection * chaseSpeed, ForceMode.Force);
            yield return new WaitForFixedUpdate();
        }
    }
}
