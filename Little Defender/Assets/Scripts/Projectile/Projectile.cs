using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Vector3 direction;
    protected float speed;
    protected float damage;
    protected float lifetime;

    public void Initialize(Vector3 direction, float speed, float damage, float lifetime)
    {
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;

        Lunch();
    }

    protected virtual void Lunch()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    protected abstract void OnHit();
}
