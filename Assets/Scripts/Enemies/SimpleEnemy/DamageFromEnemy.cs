using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromEnemy : MonoBehaviour
{
    public delegate void PlayerHit();
    public static event PlayerHit PlayerTakeDamage; 
    private bool immunity = false; 
    public float enemyImmunity = 2f;
    private Rigidbody rb;
    private float impulseScalar;
    private float impulseDuration;

    public AudioSource hitSound;

    void Update()
    {
        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.1f;//size of the rays
        float halfLength = col.bounds.extents.x + 0.11f;
        
        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            if (hit.collider.CompareTag("Enemy")) //Gets the raycast for landing on the simple enemy;
            {
                Destroy(hit.collider.gameObject);
                
                StartCoroutine(DamageImmunity());
                return;
            }
        }

        if (Physics.Raycast(startPoint, Vector2.right, out hit, halfLength))
        {
            if (hit.collider.CompareTag("Enemy") && !immunity)//Gets the raycast for the side of the simple enemy;
            {   
                rb = hit.collider.GetComponent<Rigidbody>();

                Collider collider = hit.collider; //Gets a vector to add a impulse to send the enemies back.
                
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                
                StartCoroutine(ImpulseRun(direction));

                PlayerTakeDamage.Invoke();

                hitSound.Play();
                
                StartCoroutine(DamageImmunity());
                
                return;
            }
        }

        if (Physics.Raycast(startPoint, Vector2.left, out hit, halfLength))
        {
            if (hit.collider.CompareTag("Enemy") && !immunity)//Gets the raycast for the side of the simple enemy;
            {   
                rb = hit.collider.GetComponent<Rigidbody>();

                Collider collider = hit.collider;//Gets a vector to add a impulse to send the enemies back.
                
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                
                StartCoroutine(ImpulseRun(direction));

                PlayerTakeDamage.Invoke();
                
                hitSound.Play();

                StartCoroutine(DamageImmunity());

                return;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "FrogTongue" && !immunity){
            PlayerTakeDamage.Invoke();
            hitSound.Play();
            StartCoroutine(DamageImmunity());
        }

        if(other.gameObject.tag == "Beam" && !immunity){
            PlayerTakeDamage.Invoke();
            StartCoroutine(DamageImmunity());
        }
    }

    private IEnumerator DamageImmunity()
    {
        immunity = true;
        yield return new WaitForSeconds(enemyImmunity); //Gives player immunity from enemy hits
        immunity = false;
    }

    private IEnumerator ImpulseRun(Vector3 knockback)
    {
        rb.AddForce(knockback * impulseScalar, ForceMode.Impulse);//Hits the impulse then zeros out after desired time.
        yield return new WaitForSeconds(impulseDuration);
        rb.velocity = Vector3.zero;
    }
}