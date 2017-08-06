using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    Health health;    

    private void Awake()
    {
        health = GetComponent<Health>();        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't damage when just rotating
        //if (collision.otherRigidbody && collision.otherRigidbody.velocity == Vector2.zero)
        //    return;

        var otherHealth = collision.collider.gameObject.GetComponent<Health>();

        if (otherHealth)
        {
            if (!otherHealth.TakeDamage(health.CurrentHealth / 2))
            {
                Destroy(gameObject);
            }
        }
        health.TakeDamage(health.CurrentHealth / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
