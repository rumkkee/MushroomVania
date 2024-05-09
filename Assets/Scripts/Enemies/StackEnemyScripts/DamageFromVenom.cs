using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromVenom : MonoBehaviour
{
    public delegate void PlayerHit();
    public static event PlayerHit PlayerTakeDamage;

    void Update()
    {
        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.1f;

        RaycastHit hit;

        // Check for collisions downwards
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight) && hit.collider.CompareTag("Venom"))
        {
            PlayerTakeDamage.Invoke();
        }
        else if (Physics.Raycast(startPoint, Vector2.right, out hit, halfHeight) && hit.collider.CompareTag("Venom"))
        {
            PlayerTakeDamage.Invoke();
        }
        else if (Physics.Raycast(startPoint, Vector2.left, out hit, halfHeight) && hit.collider.CompareTag("Venom"))
        {
            PlayerTakeDamage.Invoke();
        }
        else if (Physics.Raycast(startPoint, Vector2.up, out hit, halfHeight) && hit.collider.CompareTag("Venom"))
        {
            PlayerTakeDamage.Invoke();
        }
    }
}
