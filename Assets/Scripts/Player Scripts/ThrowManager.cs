using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ThrowManager : MonoBehaviour
{

    public Spore sporePrefab;
    public float throwForce;
    public float sporeFlightDuration;

    /// <summary>
    /// The time the player must wait after a spore has been destroyed until they can throw again
    /// </summary>
    public float cooldownDuration;
    private bool onCooldown;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        onCooldown = false;
        //Spore.OnSporeDestroyed += StartCooldown;
    }

    /// <summary>
    /// Immediately throws the spore in the normalized direction of (mousePos - playerPos)
    /// </summary>
    private void OnChargeThrow(InputValue value)
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));

        Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;

        if(Spore.instance == null && !onCooldown)
        {
            Spore sporeThrown = Instantiate(sporePrefab, transform.position, Quaternion.identity);
            sporeThrown.AddImpulse(throwDirection, throwForce);
            StartCoroutine(sporeThrown.Lifespan(sporeFlightDuration));
        }
        //Debug.Log(throwDirection);
    }

    private void StartCooldown() => StartCoroutine(StartCooldownHelper());

    private IEnumerator StartCooldownHelper()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}
