using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDynamite : Projectile
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            IDamageable targetHealth = other.GetComponent<IDamageable>();
            targetHealth.TakeDamage(damage);
            OnHit();
        }
    }

    protected override void OnHit()
    {
        AudioManager.Instance.Play("Explosion");
        EffectManager.Instance.Play("Explosion", transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
