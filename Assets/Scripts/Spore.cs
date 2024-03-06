using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{

    public delegate void SporeCollided();
    public static event SporeCollided OnSporeDestroyed;

    public static Spore instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddImpulse(Vector2 direction, float speed)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    public IEnumerator Lifespan(float lifeDuration)
    {
        yield return new WaitForSeconds(lifeDuration);
        if(this != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.gameObject.GetComponent<Movement>();
        if(player == null)
        {
            SporeCollisionEvents();
            Destroy(gameObject);
        }  
    }

    protected virtual void SporeCollisionEvents()
    {
            OnSporeDestroyed();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
