using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAnimation : MonoBehaviour
{
    protected DirectionState currentDirection;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Transform target;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    protected void Play(string name)
    {
        animator.Play(name, 0, 0f);
    }
}
