using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorchAnimation : EnemyAnimation
{
    private EnemyTorchState currentState;

    public void PlayAnimation(EnemyTorchState state, DirectionState direction)
    {
        string animationName;

        switch (state)
        {
            case EnemyTorchState.Idle:
            case EnemyTorchState.Run:
            case EnemyTorchState.Attack:
                animationName = $"Torch_{state}_{direction}";
                break;
            default:
                animationName = $"Torch_Idle_Left";
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
        PlayAnimation(EnemyTorchState.Idle, direction);
    }

    public void PlayRunAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyTorchState.Run, direction);
    }

    public void PlayAttackAnimation()
    {
        Vector2 direction = target.position - transform.position;
        DirectionState directionState;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            directionState = direction.y > 0 ? DirectionState.Right : DirectionState.Left;
        }
        else
        {
            directionState = direction.y > 0 ? DirectionState.Up : DirectionState.Down;
        }

        PlayAnimation(EnemyTorchState.Attack, directionState);
    }
}

public enum EnemyTorchState
{
    Idle,
    Run,
    Attack
}
