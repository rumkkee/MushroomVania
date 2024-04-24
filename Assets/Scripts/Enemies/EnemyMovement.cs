using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody rb;
    [SerializeField] private float knockbackForce = 6;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeKnockback(Vector2 direction)
    {
        Vector2 normalized = direction.normalized;
        rb.velocity = Vector3.zero;
        rb.AddForce(normalized * knockbackForce, ForceMode.Impulse);
    }
}
