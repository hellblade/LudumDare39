using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
    public float healthGained;

    protected override System.Type RequiredComponent
    {
        get
        {
            return typeof(Health);
        }
    }

    protected override bool OnPickup(Component target)
    {
        var health = target as Health;

        if (!health || health.AtFullHealth())
            return false;

        health.Heal(healthGained);
        return true;
    }
}

