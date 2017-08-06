using System;
using System.Collections.Generic;

using UnityEngine;


public class HealthUpgrade : Pickup
{
    public int maxHpGain = 20;

    protected override Type RequiredComponent
    {
        get
        {
            return typeof(PlayerController);
        }
    }

    protected override bool OnPickup(Component target)
    {
        target.GetComponent<Health>().AddMaxHealth(maxHpGain);
        return true;
    }
}

