using System.Collections;
using UnityEngine;

public class Spore : MonoBehaviour
{

    public delegate void SporeCollided();
    public static event SporeCollided OnSporeDestroyed;

    [Range(0, -10)]
    public float customGravity;
    private Vector3 gravity;

    [Range(0, 20)]
    public float throwSpeed;

    private Rigidbody rb;

    public static Spore instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        rb.useGravity = false;
        gravity = new Vector3(0, customGravity, 0);
    }

    public void ApplyCustomGravity()
    {
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    public void FixedUpdate()
    {
        ApplyCustomGravity();
    }

    public void AddImpulse(Vector2 direction, float speed)
    {
        rb = GetComponent<Rigidbody>();
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
        if (other.isTrigger) { return; }

        if(!other.CompareTag("Player"))
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

    public float GetCustomGravity() => customGravity;
    public float GetThrowSpeed() => throwSpeed;
}
