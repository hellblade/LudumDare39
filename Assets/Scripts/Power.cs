using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    public int maxPower;
    public float CurrentPower { get; private set; }
    public UnityEvent depleted;

    private void Awake()
    {
        CurrentPower = maxPower;
    }

    public void SetMax(int amount)
    {
        maxPower = amount;
        CurrentPower = amount;
    }

    public void AddMax(int amount)
    {
        maxPower += amount;
    }

    public bool UsePower(float amount)
    {                
        CurrentPower -= amount;

        if (CurrentPower <= 0 && amount > 0)
        {
            depleted.Invoke();
            return false;
        }

        return true;
    }

    public void GainPower(float amount)
    {
        CurrentPower += amount;
        if (CurrentPower > maxPower)
        {
            CurrentPower = maxPower;
        }
    }
}