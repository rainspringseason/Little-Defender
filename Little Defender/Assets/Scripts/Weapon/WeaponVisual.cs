using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private int baseSortingOrder;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        baseSortingOrder = spriteRenderer.sortingOrder;
    }

    public void HandleFlip(PlayerInput input)
    {
        Vector3 mousePosition = input.GetMousePosition();
        spriteRenderer.flipY = mousePosition.x < transform.position.x ? true : false;
    }

    public void HandleSortingLayer(float angle)
    {
        if (angle < 135 && angle > 45)
        {
            spriteRenderer.sortingOrder = baseSortingOrder - 1;
        }
        else
        {
            spriteRenderer.sortingOrder = baseSortingOrder + 1;
        }
    }
}
