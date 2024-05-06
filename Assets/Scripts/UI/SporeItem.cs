using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SporeType
{
    Cordyceps,
    Fire,
    Teleport
}

public class SporeItem : MonoBehaviour
{
    public Sprite sporeSprite;

    public GameObject sporePrefab;

    public SporeType sporeType;

    public AudioClip SelectSound;

    public float maxCharge = 100f;
    public float currentCharge;

    public float chargeTakenPerUse;
    public float chargeDelay;
    public float rechargeRatePerSec;
    

    private void Start()
    {
        if(currentCharge < maxCharge)
        {
            Debug.Log("Starting charge from: " + gameObject.name);
            StartCoroutine(CheckCharge());
        }
    }

    private IEnumerator CheckCharge()
    {
        if(currentCharge < maxCharge)
        {
            yield return StartCoroutine(ChargeDelay());
            yield return StartCoroutine(Charge());
        }
    }

    private IEnumerator ChargeDelay()
    {
        Debug.Log("Entered ChargeDelay");
        float timeRemaining = chargeDelay;
        do
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        } while (timeRemaining > 0);
    }

    private IEnumerator Charge()
    {
        Debug.Log("Entered Charge");
        do
        {
            currentCharge += rechargeRatePerSec * Time.deltaTime;
            yield return null;
        } while (currentCharge < maxCharge);
        currentCharge = maxCharge;
    }
}