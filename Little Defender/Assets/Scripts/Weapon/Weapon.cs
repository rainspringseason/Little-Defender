using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] protected float weaponCooldown = 0.25f;

    [Header("Projectile Settings")]
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Transform projectileFirePoint;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileLifetime;

    protected PlayerInput playerInput;
    protected WeaponVisual weaponVisual;
    protected float cooldownTimer;
    protected Vector3 firePointStart;

    protected virtual void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        weaponVisual = GetComponent<WeaponVisual>();
    }

    protected virtual void Start()
    {
        firePointStart = projectileFirePoint.localPosition;
    }

    protected virtual void Update()
    {
        HandleRotation();
        HandleFirePoint();
        weaponVisual.HandleFlip(playerInput);
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = playerInput.GetMousePosition();
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        weaponVisual.HandleSortingLayer(angle);
    }

    private void HandleFirePoint()
    {
        Vector3 mousePosition = playerInput.GetMousePosition();
        Vector3 firePoint = firePointStart;

        if (firePointStart.y < 0)
        {
            firePoint.y = mousePosition.x < transform.position.x ? Mathf.Abs(firePointStart.y) : -Mathf.Abs(firePointStart.y);
        }
        else
        {
            firePoint.y = mousePosition.x < transform.position.x ? -Mathf.Abs(firePointStart.y) : Mathf.Abs(firePointStart.y);
        }

        projectileFirePoint.localPosition = firePoint;
    }

    public void ShootInput()
    {
        if (Time.time >= cooldownTimer)
        {
            Shoot();
            cooldownTimer = Time.time + weaponCooldown;
        }
    }

    protected abstract void Shoot();
}
