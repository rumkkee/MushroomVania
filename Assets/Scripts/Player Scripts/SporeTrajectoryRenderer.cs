using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeTrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private ThrowTest throwTest;

    public int numPoints = 20;
    private float timeStep = 0.1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        throwTest = GetComponentInParent<ThrowTest>();
    }

    private void OnEnable()
    {
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        // accessing the gravity, throwSpeed, and thrown direction
        float customGravity = throwTest.currentSpore.GetCustomGravity();
        Vector3 gravity = new Vector3(0, customGravity, 0);
        float throwForce = throwTest.currentSpore.GetThrowSpeed();
        Vector3 direction = throwTest.direction.normalized;

        Vector3 launchVelocity = direction * throwForce;
        Vector3 position = Vector3.zero;

        // Collision will be checked between position and nextPos
        Vector3 nextVel = launchVelocity;
        Vector3 nextPos = position;

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPosition(0, position);
        for (int i = 1; i < numPoints; i++)
        {
            //Vector3 nextPosition
            nextVel += gravity * timeStep;
            nextPos += launchVelocity * timeStep;

            lineRenderer.SetPosition(i, nextPos);
            // Raycast between pos and nextPos
            RaycastHit hit;

            // if an object was hit, break
            Vector3 positionWorld = this.transform.position + position;
            Vector3 nextPositionWorld = this.transform.position + nextPos;
            if (Physics.Linecast(positionWorld, nextPositionWorld, out hit))
            {
                Debug.DrawLine(positionWorld, nextPositionWorld, Color.green);
                if (!hit.collider.isTrigger && !hit.collider.CompareTag("Player"))
                { 
                    lineRenderer.positionCount = i+1;              
                    break;
                }
            }

            // Storing this iteration's nextPos into position
            launchVelocity = nextVel;
            position = nextPos;
        }
        
    }

    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
    }
}
