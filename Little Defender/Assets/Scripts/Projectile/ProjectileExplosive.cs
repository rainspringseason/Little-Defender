using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosive : Projectile
{
    [Header("Explosion Settings")]
    public float explosionRadius = 3f;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable targetHealth = other.GetComponent<IDamageable>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            OnHit();
        }
    }

    protected override void OnHit()
    {
        AudioManager.Instance.Play("Hit");
        AudioManager.Instance.Play("Explosion");
        EffectManager.Instance.Play("Explosion", transform.position, Quaternion.identity);

        Explode();

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
