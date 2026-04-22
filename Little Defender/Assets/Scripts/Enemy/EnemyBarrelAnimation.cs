using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrelAnimation : EnemyAnimation
{
    private EnemyBarrelState currentState;

    public void PlayAnimation(EnemyBarrelState state, DirectionState direction)
    {
        string animationName;

        switch (state)
        {
            case EnemyBarrelState.IdleIn:
            case EnemyBarrelState.In:
            case EnemyBarrelState.Out:
            case EnemyBarrelState.Run:
            case EnemyBarrelState.Explode:
                animationName = $"Barrel_{state}_{direction}";
                break;
            default:
                animationName = $"Barrel_IdleIn_Left";
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

    public void PlayIdleInAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyBarrelState.IdleIn, direction);
    }

    public void PlayInAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyBarrelState.In, direction);
    }

    public void PlayOutAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyBarrelState.Out, direction);
    }

    public void PlayRunAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyBarrelState.Run, direction);
    }

    public void PlayExplodeAnimation()
    {
        DirectionState direction = spriteRenderer.flipX ? DirectionState.Left : DirectionState.Right;
        PlayAnimation(EnemyBarrelState.Explode, direction);
    }
}

public enum EnemyBarrelState
{
    IdleIn,
    In,
    Out,
    Run,
    Explode
}
