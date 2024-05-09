using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBodyHealth : MonoBehaviour
{
    private void OnDestroy()
    {
        StackEnemy stackEnemy = GetComponentInParent<StackEnemy>();
        stackEnemy.bodyDestroyed();
    }
}
