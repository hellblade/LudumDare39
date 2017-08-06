using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangedWeapon))]
public class AiShooter : MonoBehaviour
{
    PlayerController target;
    RangedWeapon weapon;


    private void Awake()
    {
        weapon = GetComponent<RangedWeapon>();
        target = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector3.Distance(target.transform.position, transform.position) <= weapon.projectile.maxDistance)
        {
            weapon.Fire();
        }
    }
}

