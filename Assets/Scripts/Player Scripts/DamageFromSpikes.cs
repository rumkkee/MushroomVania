using UnityEngine;

public class DamageFromSpikes : MonoBehaviour
{
    public delegate void PlayerHit();
    public static event PlayerHit PlayerTakeDamage;

    public AudioSource hitSound;

    void Update()
    {
        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.1f;

        RaycastHit hit;

        // Check for collisions downwards
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight) && hit.collider.CompareTag("Spikes"))
        {
            PlayerTakeDamage.Invoke();
            hitSound.Play();
        }

        // Check for collisions on the right
        else if (Physics.Raycast(startPoint, Vector2.right, out hit, halfHeight) && hit.collider.CompareTag("Spikes"))
        {
            PlayerTakeDamage.Invoke();
            hitSound.Play();
        }

        // Check for collisions on the left
        else if (Physics.Raycast(startPoint, Vector2.left, out hit, halfHeight) && hit.collider.CompareTag("Spikes"))
        {
            PlayerTakeDamage.Invoke();
            hitSound.Play();
        }

        // Check for collisions upwards
        else if (Physics.Raycast(startPoint, Vector2.up, out hit, halfHeight) && hit.collider.CompareTag("Spikes"))
        {
            PlayerTakeDamage.Invoke();
            hitSound.Play();
        }

        // Kill the Player if he is below y = -50
        if (transform.position.y < -50)
        {
            PlayerTakeDamage.Invoke();
        }
    }
}