using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 500;
    private int currentHealth;
    public GameObject healthDrop;
    private int secondsForFire = 5;
    private bool onFire = false;

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
        SpiderEnemyMovement web = gameObject.GetComponent<SpiderEnemyMovement>();
        if(Random.Range(1, 6) == 1){
            Instantiate(healthDrop, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void TakeFireDamage(int damageReceived)
    {
        Debug.Log("enemy hit");
        if(!onFire)
        {
            onFire = true;
            StartCoroutine(FireDamage(damageReceived));
        } else {
            secondsForFire = 5;
        }
    }

    private IEnumerator FireDamage(int damageReceived)
    {
        //This is just a cooldown on the time between swings when previous swing finishes.
        currentHealth -= damageReceived;
        if(currentHealth <= 0)
        {
            OnDefeat();
        }
        yield return new WaitForSeconds(1);
        if(secondsForFire > 0){
            secondsForFire--;
            StartCoroutine(FireDamage(damageReceived));
        } else {
            secondsForFire = 5;
            onFire = false;
        }
    }

}
