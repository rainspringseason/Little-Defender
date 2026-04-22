using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDynamite : Enemy
{
    [Header("Dynamite Properties")]
    public Projectile projectilePrefab;
    public Transform projectileFirePoint;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 3f;

    private EnemyDynamiteAnimation enemyAnimation;
    private EnemyDynamiteState currentState = EnemyDynamiteState.Idle;

    private float attackTimer = 0f;

    protected override void Awake()
    {
        base.Awake();

        animationEvent = GetComponentInChildren<AnimationEvent>();
        health = GetComponent<Health>();
        enemyAnimation = GetComponent<EnemyDynamiteAnimation>();
        target = FindObjectOfType<Tower>().transform;

        enemyAnimation.Initialize(target);

        animationEvent.onAnimationMiddle.AddListener(OnThrow);
        animationEvent.onAnimationEnd.AddListener(TransitionToIdle);
    }

    protected override void OnDestroy()
    {
        animationEvent.onAnimationMiddle.RemoveListener(OnThrow);
        animationEvent.onAnimationEnd.RemoveListener(TransitionToIdle);
    }

    protected override void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case EnemyDynamiteState.Idle:
                HandleIdleState(distanceToTarget);
                break;
            case EnemyDynamiteState.Run:
                HandleRunState(distanceToTarget);
                break;
            case EnemyDynamiteState.Throw:
                HandleThrowState(distanceToTarget);
                break;
        }
    }

    private void HandleIdleState(float distance)
    {
        if (CanThrow(distance))
        {
            TransitionToThrow();
        }
        else if (distance > distanceToDamageTarget)
        {
            TransitionToRun();
        }
    }

    private void HandleRunState(float distance)
    {
        if (CanThrow(distance))
        {
            TransitionToThrow();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void HandleThrowState(float distance)
    {
        if (distance > distanceToDamageTarget)
        {
            TransitionToRun();
        }
    }

    private bool CanThrow(float distance)
    {
        return distance <= distanceToDamageTarget && attackTimer <= 0f;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        enemyAnimation.HandleSpriteDirection();
        enemyAnimation.PlayRunAnimation();
    }

    private void TransitionToRun()
    {
        currentState = EnemyDynamiteState.Run;
        attackTimer = 0f;
        enemyAnimation.PlayRunAnimation();
    }

    private void TransitionToThrow()
    {
        currentState = EnemyDynamiteState.Throw;
        attackTimer = cooldown;
        enemyAnimation.PlayThrowAnimation();
    }

    public void TransitionToIdle()
    {
        currentState = EnemyDynamiteState.Idle;
        enemyAnimation.PlayIdleAnimation();
    }

    public void OnThrow()
    {
        Vector3 projectileDirection = (target.position - projectileFirePoint.position).normalized;

        Projectile projectile = Instantiate(projectilePrefab, projectileFirePoint.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, projectileSpeed, damage, projectileLifetime);
    }

    public override void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
        blink.DamageEffect();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToDamageTarget);
    }
}
