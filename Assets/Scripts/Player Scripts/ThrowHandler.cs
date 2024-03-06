using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ThrowHandler : MonoBehaviour
{

    public Spore sporePrefab;
    public float throwForce;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnChargeThrow(InputValue value)
    {
        // For now, immediately throws the spore in the normalized direction of (mousePos - playerPos)
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));

        Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;

        if(Spore.instance == null)
        {
            Spore sporeThrown = Instantiate(sporePrefab, transform.position, Quaternion.identity);
            sporeThrown.AddImpulse(throwDirection, throwForce);
        }
        //Debug.Log(throwDirection);

    }
}
