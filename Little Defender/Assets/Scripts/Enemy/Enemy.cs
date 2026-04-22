using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Properties")]
    public float speed;
    public float damage;
    public float cooldown;
    public float distanceToDamageTarget;

    protected AnimationEvent animationEvent;
    protected Health health;
    protected BlinkEffect blink;
    protected Spawner spawner;
    protected Transform target;

    protected virtual void Awake()
    {
        animationEvent = GetComponentInChildren<AnimationEvent>();
        health = GetComponent<Health>();
        blink = GetComponent<BlinkEffect>();
        spawner = FindObjectOfType<Spawner>();
        target = FindObjectOfType<Tower>().transform;
    }

    protected virtual void OnEnable()
    {
        health.onDeath += OnDeath;
    }

    protected virtual void OnDisable()
    {
        health.onDeath -= OnDeath;
    }

    protected virtual void OnDestroy()
    {
        health.onDeath -= OnDeath;
    }

    protected abstract void Update();

    public abstract void TakeDamage(float amount);

    protected virtual void OnDeath()
    {
        EffectManager.Instance.Play("Death", transform.position, Quaternion.identity);
        spawner.OnEnemyDestroyed();
        Destroy(gameObject);
    }
}
