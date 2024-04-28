using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 500;
    private int currentHealth;
    public GameObject healthDrop;

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
        if(Random.Range(1, 6) == 1){
            Instantiate(healthDrop, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

}
