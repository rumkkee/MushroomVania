using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMasterHealth : MonoBehaviour
{
    private int maxHealth = 500;
    private int currentHealth;
    public StackEnemy stackEnemy;

    private void Awake()
    {
        maxHealth = stackEnemy.length * 50;
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

    public void HeadDestroyed()
    {
        currentHealth = 0;
        OnDefeat();
    }

    private void OnDefeat()
    {
        Destroy(this.gameObject);
    }

}
