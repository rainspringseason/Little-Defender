using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaser : Projectile
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable targetHealth = other.GetComponent<IDamageable>();
            targetHealth.TakeDamage(damage);
            OnHit();
        }
    }

    protected override void OnHit()
    {
        AudioManager.Instance.Play("Hit");
    }
}
