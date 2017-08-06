using System;
using System.Collections.Generic;

using UnityEngine;


public class BadZone : MonoBehaviour
{
    public float powerDrain = 1.0f;
    public float hpDrain = 1.0f;

    public Vector2 moveVector;

    float leftToWait;
    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    public void SetWaitTime(float time)
    {
        leftToWait = time;
    }

    private void FixedUpdate()
    {
        if (leftToWait > 0)
        {
            leftToWait -= Time.deltaTime;
            return;
        }

        pos.x += moveVector.x;
        pos.y += moveVector.y;

        transform.position = pos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var hp = collision.gameObject.GetComponent<Health>();
        var power = collision.gameObject.GetComponent<Power>();        

        if (power && power.CurrentPower > 0)
        {
            power.UsePower(powerDrain);
        }
        else if (hp)
        {
            hp.TakeDamage(hpDrain);
        }
    }

}
