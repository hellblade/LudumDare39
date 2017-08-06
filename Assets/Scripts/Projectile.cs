using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public float maxDistance = 500;

    Vector3 initialPositon;

    public void SetOwner(GameObject owner)
    {
        Collider2D col1 = GetComponent<Collider2D>();
        Collider2D col2 = owner.GetComponent<Collider2D>();

        if (!col2)
        {
            col2 = owner.GetComponentInParent<Collider2D>();
        }

        Physics2D.IgnoreCollision(col1, col2);
    }

    private void Update()
    {
        if (initialPositon == Vector3.zero)
        {
            initialPositon = transform.position;
        }

        if (Vector3.Distance(initialPositon, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(Random.Range(minDamage, maxDamage));
        }

        // Projectiles can go through each other
        if (!collision.gameObject.GetComponent<Projectile>())
        {
            Destroy(gameObject);
        }
    }
}