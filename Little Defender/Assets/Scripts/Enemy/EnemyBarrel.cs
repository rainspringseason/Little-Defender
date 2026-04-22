using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrel : Enemy
{
    [Header("Barrel Properties")]
    public float moveDuration = 2f;
    public float explosionRange = 5f;

    private EnemyBarrelAnimation enemyAnimation;
    private EnemyBarrelState currentState = EnemyBarrelState.Run;

    private float stateTimer = 0f;
    private bool isImmune = false;

    protected override void Awake()
    {
        base.Awake();

        enemyAnimation = GetComponent<EnemyBarrelAnimation>();

        enemyAnimation.Initialize(target);
        animationEvent.onAnimationEnd.AddListener(OnExplode);
    }

    protected override void OnDestroy()
    {
        animationEvent.onAnimationEnd.RemoveListener(OnExplode);
    }

    protected override void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= distanceToDamageTarget && currentState != EnemyBarrelState.Explode)
        {
            TransitionToExplode();
            return;
        }

        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case EnemyBarrelState.IdleIn:
                HandleIdleInState();
                break;
            case EnemyBarrelState.In:
                HandleInState();
                break;
            case EnemyBarrelState.Out:
                HandleOutState();
                break;
            case EnemyBarrelState.Run:
                HandleRunState();
                break;
        }
    }

    private void HandleIdleInState()
    {
        if (stateTimer >= cooldown)
        {
            TransitionToOut();
        }
    }

    private void HandleInState()
    {
        if (stateTimer >= 0.5f)
        {
            TransitionToIdleIn();
        }
    }

    private void HandleOutState()
    {
        if (stateTimer >= 0.5f)
        {
            TransitionToRun();
        }
    }

    private void HandleRunState()
    {
        MoveTowardsTarget();

        if (stateTimer >= moveDuration)
        {
            TransitionToIn();
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        enemyAnimation.HandleSpriteDirection();
        enemyAnimation.PlayRunAnimation();
    }

    private void TransitionToIdleIn()
    {
        currentState = EnemyBarrelState.IdleIn;
        isImmune = true;
        stateTimer = 0f;
        enemyAnimation.PlayIdleInAnimation();
    }

    private void TransitionToIn()
    {
        currentState = EnemyBarrelState.In;
        isImmune = true;
        stateTimer = 0f;
        enemyAnimation.PlayInAnimation();
    }

    private void TransitionToOut()
    {
        currentState = EnemyBarrelState.Out;
        isImmune = false;
        stateTimer = 0f;
        enemyAnimation.PlayOutAnimation();
    }

    private void TransitionToRun()
    {
        currentState = EnemyBarrelState.Run;
        isImmune = false;
        stateTimer = 0f;
        enemyAnimation.PlayRunAnimation();
    }

    private void TransitionToExplode()
    {
        currentState = EnemyBarrelState.Explode;
        isImmune = false;
        stateTimer = 0f;
        enemyAnimation.PlayExplodeAnimation();
    }

    public void OnExplode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach (Collider2D collider in colliders)
        {
            Tower targetHealth = collider.GetComponent<Tower>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        AudioManager.Instance.Play("Explosion");
        EffectManager.Instance.Play("Explosion", transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void TakeDamage(float amount)
    {
        if (!isImmune)
        {
            health.TakeDamage(amount);
            blink.DamageEffect();
        }
    }

    protected override void OnDeath()
    {
        TransitionToExplode();
    }

    public bool IsImmune()
    {
        return isImmune;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToDamageTarget);
    }
}
