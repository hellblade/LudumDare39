using System;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceTarget : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

