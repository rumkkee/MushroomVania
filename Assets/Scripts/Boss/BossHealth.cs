using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 500;
    public int currentHealth;
    private int secondsForFire = 5;
    private bool onFire = false;

    public AudioSource hitSound;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageReceived)
    {
        Debug.Log("enemy hit");
        currentHealth -= damageReceived;
        hitSound.Play();
        if (currentHealth <= 0)
        {
            OnDefeat();
        }
    }

    private void OnDefeat()
    {
        Destroy(this.gameObject);
    }

    public void TakeFireDamage(int damageReceived)
    {
        Debug.Log("enemy hit");
        if (!onFire)
        {
            onFire = true;
            StartCoroutine(FireDamage(damageReceived));
        }
        else
        {
            secondsForFire = 5;
        }
    }

    private IEnumerator FireDamage(int damageReceived)
    {
        //This is just a cooldown on the time between swings when previous swing finishes.
        currentHealth -= damageReceived;
        hitSound.Play();
        if (currentHealth <= 0)
        {
            OnDefeat();
        }
        yield return new WaitForSeconds(1);
        if (secondsForFire > 0)
        {
            secondsForFire--;
            StartCoroutine(FireDamage(damageReceived));
        }
        else
        {
            secondsForFire = 5;
            onFire = false;
        }
    }

}
