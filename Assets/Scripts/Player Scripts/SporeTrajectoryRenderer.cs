using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeTrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private ThrowTest throwTest;

    public int numPoints = 10;
    public float timeStep = 0.1f;

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
        // access the throw direction and force
        float customGravity = throwTest.sporePrefab.GetCustomGravity();
        Vector3 gravity = new Vector3(0, customGravity, 0);
        Debug.Log(customGravity);
        float throwForce = throwTest.throwForce;
        Vector3 direction = throwTest.direction.normalized;
        direction = new Vector3(direction.y, -direction.x, 0);

        Vector3 launchVelocity = direction * throwForce;

        Vector3 position = Vector3.zero;
        for(int i = 0; i < numPoints; i++)
        {
            lineRenderer.SetPosition(i, position);
            position += launchVelocity * timeStep;
            launchVelocity += gravity * timeStep;
        }
        
    }

    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
    }
}
