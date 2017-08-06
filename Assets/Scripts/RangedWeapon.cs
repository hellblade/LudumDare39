using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Power))]
class RangedWeapon : MonoBehaviour
{
    public Projectile projectile;
    [SerializeField] Transform firePosition;
    [SerializeField] float speed;
    [SerializeField] float timeBetweenFires;
    [SerializeField] float energyUsed;

    public UnityEvent OnFire;


    float timeTillFire;
    Power energy;

    private void Awake()
    {
        energy = GetComponent<Power>();
    }

    public void Fire()
    {
        if (timeTillFire > 0 || !energy.UsePower(energyUsed))
            return;

        if (OnFire != null)
        {
            OnFire.Invoke();
        }

        var proj = Instantiate<Projectile>(projectile, firePosition.position,  firePosition.rotation);

        proj.SetOwner(gameObject);
        var body = proj.GetComponent<Rigidbody2D>();
        body.velocity = firePosition.up * speed;

        timeTillFire = timeBetweenFires;        
    }

    private void Update()
    {
        if (timeTillFire > 0)
        {
            timeTillFire -= Time.deltaTime;
        }
    }
}

