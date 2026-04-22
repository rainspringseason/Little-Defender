using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDynamiteAnimation : EnemyAnimation
{
    private EnemyDynamiteState currentState;

    public void PlayAnimation(EnemyDynamiteState state, DirectionState direction)
    {
        string animationName;

        switch (state)
        {
            case EnemyDynamiteState.Idle:
            case EnemyDynamiteState.Run:
            case EnemyDynamiteState.Throw:
                animationName = $"Dynamite_{state}_{direction}";
                break;
            default:
                animationName = $"Dynamite_Idle_Left";
                break;
        }

        if (currentState != state || currentDirection != direction)
        {
            currentState = state;
            currentDirection = direction;
            Play(animationName);
        }
    }

    public void HandleSpriteDirection()
    {
        spriteRenderer.flipX = target.position.x < transform.position.x ? true : false;
    }

    public void PlayIdleAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyDynamiteState.Idle, direction);
    }

    public void PlayRunAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyDynamiteState.Run, direction);
    }

    public void PlayThrowAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyDynamiteState.Throw, direction);
    }
}

public enum EnemyDynamiteState
{
    Idle,
    Run,
    Throw
}
