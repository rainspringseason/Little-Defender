using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerState currentState;
    private DirectionState currentDirection;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation(PlayerState state, DirectionState direction)
    {
        string animationName;

        switch (state)
        {
            // Player animation have any direction
            case PlayerState.Idle:
            case PlayerState.Walk:
                animationName = $"Player_{state}_{direction}";
                break;

            // Player animation doesnt have any direction
            case PlayerState.Hurt:
            case PlayerState.Death:
                animationName = $"Player_{state}";
                break;

            default:
                animationName = $"Player_Idle_Down";
                break;
        }

        if (currentState != state || currentDirection != direction)
        {
            currentState = state;
            currentDirection = direction;
            Play(animationName);
        }
    }

    private void Play(string name)
    {
        animator.Play(name, 0, 0f);
    }
}

public enum PlayerState
{
    Idle,
    Walk,
    Hurt,
    Death
}
