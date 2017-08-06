using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public float CurrentHealth { get; private set; }
    public UnityEvent killed;
    public bool destroyOnDeath = true;

    Power power;
    bool shieldEnabled = false;
    public float shieldEnergyDrain = 0.25f;

    internal bool AtFullHealth()
    {
        return CurrentHealth == maxHealth;
    }

    public float shieldDamageRate = 0.5f;

    public bool ShieldEnabled  => shieldEnabled;
    [SerializeField] GameObject shieldDisplay;

    public UnityEvent shieldOn;
    public UnityEvent shieldOff;

    bool hasPower;


    private void Awake()
    {
        CurrentHealth = maxHealth;
        power = GetComponent<Power>();

        hasPower = power;
    }

    private void FixedUpdate()
    {
        // Drain power from sheild use and disable if runs out...
        if (hasPower && shieldEnabled && !power.UsePower(shieldEnergyDrain))
        {
            SetShield (false);
        }
    }

    public void SetShield(bool isOn)
    {
        shieldEnabled = isOn && power.CurrentPower > 0;

        if (shieldEnabled)
        {
            shieldOn.Invoke();
        }
        else
        {
            shieldOff.Invoke();
        }

        if (shieldDisplay)
        {
            shieldDisplay.SetActive(shieldEnabled);
        }
    }

    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
        CurrentHealth = hp;
    }

    public void AddMaxHealth(int hp)
    {
        maxHealth += hp;
    }

    public bool TakeDamage(float amount)
    {
        if (shieldEnabled && power.UsePower(amount * shieldDamageRate))
        {            
            return false;
        }

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            killed.Invoke();

            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }

        return true;
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }
}