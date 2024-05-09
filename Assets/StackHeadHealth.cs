using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackHeadHealth : MonoBehaviour
{
    private void OnDestroy()
    {
        StackEnemy stackEnemy = GetComponentInParent<StackEnemy>();
        stackEnemy.headDestroyed();
    }
}
