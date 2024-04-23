using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageReceived)
    {
        Debug.Log("enemy hit");
        currentHealth -= damageReceived;
        if(currentHealth <= 0)
        {
            OnDefeat();
        }
    }

    private void OnDefeat()
    {
        Destroy(this.gameObject);
    }

}
