using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomAttackManager : MonoBehaviour
{
    [SerializeField] private VenomBall venomBallPrefab;

    private void Start()
    {
        VenomBallVolley();
    }

    // Shoots 5 balls of venom
    private void VenomBallVolley()
    {
        float throwSpeed = 3f;
        Vector2 throwDirection = Vector2.down;
        float rotationDegrees = 30f;
        Vector3 rotationAxis = Vector3.forward;

        VenomBall venomBall;
        venomBall = Instantiate(venomBallPrefab, transform.position, Quaternion.identity);
        venomBall.SetVelocity(throwDirection * throwSpeed);

        for(int k = 0; k < 2; k++)
        {
            for(int i = 1; i < 3; i++)
            {
                Vector3 rotatedVector = Quaternion.AngleAxis(rotationDegrees * i, rotationAxis) * throwDirection;
                venomBall = Instantiate(venomBallPrefab, transform.position, Quaternion.identity);
                venomBall.SetVelocity(rotatedVector * throwSpeed);
            }
            rotationDegrees = -rotationDegrees;
        }


    }

}
