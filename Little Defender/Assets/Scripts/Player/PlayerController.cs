using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private DirectionState currentDirection = DirectionState.Down;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        HandleFacing();
        HandleFacingDirectionByCursor();
    }

    private void HandleFacing()
    {
        if (playerInput.Move == Vector2.zero)
        {
            playerAnimation.PlayAnimation(PlayerState.Idle, currentDirection);
        }
    }

    private void HandleFacingDirectionByCursor()
    {
        Vector3 mousePosition = playerInput.GetMousePosition();
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 135 && angle > 45)
        {
            currentDirection = DirectionState.Up;
        }
        else if (angle < -45 && angle > -135)
        {
            currentDirection = DirectionState.Down;
        }
        else if ((angle <= -135) || (angle >= 135))
        {
            currentDirection = DirectionState.Left;
        }
        else if (angle <= 45 && angle >= -45)
        {
            currentDirection = DirectionState.Right;
        }

        FaceDirection(currentDirection);
    }

    private void FaceDirection(DirectionState direction)
    {
        switch (direction)
        {
            case DirectionState.Up:
                //  Do something when Direction Up
                break;
            case DirectionState.Down:
                //  Do something when Direction Down
                break;
            case DirectionState.Left:
                spriteRenderer.flipX = true;
                break;
            case DirectionState.Right:
                spriteRenderer.flipX = false;
                break;
            default:
                break;
        }
    }

    public void SetDirection(DirectionState newDirection)
    {
        currentDirection = newDirection;
        FaceDirection(currentDirection);
    }
}

public enum DirectionState
{
    Up,
    Down,
    Left,
    Right
}
