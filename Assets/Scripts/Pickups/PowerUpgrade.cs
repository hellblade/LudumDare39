using System;
using System.Collections.Generic;

using UnityEngine;


public class PowerUpgrade : Pickup
{
    public int maxPowerGain = 50;

    protected override Type RequiredComponent
    {
        get
        {
            return typeof(PlayerController);
        }
    }

    protected override bool OnPickup(Component target)
    {
        target.GetComponent<Power>().AddMax(maxPowerGain);
        return true;
    }
}

