using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpulse : MonoBehaviour
{
    public float impulseDuration = 1f;
    public float impulseScalar = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            Collider collider = collision.collider;
            Vector3 direction = (transform.position - collider.transform.position).normalized;//Makes impulse on enemy when hit with sword.
            StartCoroutine(ImpulseRun(direction));
            return;
        }
    }
    private IEnumerator ImpulseRun(Vector3 knockback)
    {
        rb.AddForce(knockback * impulseScalar, ForceMode.Impulse);
        yield return new WaitForSeconds(impulseDuration);//After time it reverts back to normal movement
        rb.velocity = Vector3.zero;
    }
}
