using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorch : Enemy
{
    [Header("Torch Properties")]
    public Transform attackPoint;
    public float attackRange = 1f;

    private EnemyTorchAnimation enemyAnimation;
    private EnemyTorchState currentState = EnemyTorchState.Idle;

    private float attackTimer = 0f;

    protected override void Awake()
    {
        base.Awake();

        enemyAnimation = GetComponent<EnemyTorchAnimation>();

        enemyAnimation.Initialize(target);

        animationEvent.onAnimationMiddle.AddListener(OnAttack);
        animationEvent.onAnimationEnd.AddListener(TransitionToIdle);
    }

    protected override void OnDestroy()
    {
        animationEvent.onAnimationMiddle.RemoveListener(OnAttack);
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
            case EnemyTorchState.Idle:
                HandleIdleState(distanceToTarget);
                break;
            case EnemyTorchState.Run:
                HandleRunState(distanceToTarget);
                break;
            case EnemyTorchState.Attack:
                HandleAttackState(distanceToTarget);
                break;
        }
    }

    private void HandleIdleState(float distance)
    {
        if (CanAttack(distance))
        {
            TransitionToAttack();
        }
        else if (distance > distanceToDamageTarget)
        {
            TransitionToRun();
        }
    }

    private void HandleRunState(float distance)
    {
        if (CanAttack(distance))
        {
            TransitionToAttack();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void HandleAttackState(float distance)
    {
        if (distance > distanceToDamageTarget)
        {
            TransitionToRun();
        }
    }

    private bool CanAttack(float distance)
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
        currentState = EnemyTorchState.Run;
        attackTimer = 0f;
        enemyAnimation.PlayRunAnimation();
    }

    private void TransitionToAttack()
    {
        currentState = EnemyTorchState.Attack;
        attackTimer = cooldown;
        enemyAnimation.PlayAttackAnimation();
    }

    public void TransitionToIdle()
    {
        currentState = EnemyTorchState.Idle;
        enemyAnimation.PlayIdleAnimation();
        attackPoint.localPosition = Vector3.zero;
    }

    public void OnAttack()
    {
        Vector2 direction = target.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            attackPoint.position = new Vector3(attackPoint.position.x + (direction.x > 0 ? 1 : -1), 0, 0);
        }
        else
        {
            attackPoint.position = new Vector3(0, attackPoint.position.y + (direction.y > 0 ? 1 : -1), 0);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D collider in colliders)
        {
            Tower targetHealth = collider.GetComponent<Tower>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
                AudioManager.Instance.Play("Hit");
            }
        }
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
