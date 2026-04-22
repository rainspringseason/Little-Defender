using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{
    private Health health;
    private BlinkEffect blink;

    private void Awake()
    {
        health = GetComponent<Health>();
        blink = GetComponent<BlinkEffect>();
    }

    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
        blink.DamageEffect();
    }

    public bool IsDead()
    {
        return health.IsDead;
    }
}
