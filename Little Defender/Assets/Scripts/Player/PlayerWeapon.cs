using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform weaponParent;
    public float switchCooldown = 2f;
    public List<Weapon> weapons;

    private PlayerInput playerInput;
    private Weapon currentWeapon;
    private int currentWeaponIndex = 0;
    private float switchCooldownTimer = 0f;
    private bool isSwitchingWeapon = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        if (weapons.Count > 0)
        {
            EquipWeapon(0);
        }
    }

    private void Update()
    {
        HandleShooting();
        HandleWeaponSwitching();
        HandleSwitchCooldown();
    }

    private void HandleShooting()
    {
        if (playerInput.Shoot && currentWeapon != null)
        {
            currentWeapon.ShootInput();
        }
    }

    private void HandleWeaponSwitching()
    {
        if (isSwitchingWeapon) return;

        if (playerInput.ScrollMouse > 0f) SwitchWeapon(1);
        else if (playerInput.ScrollMouse < 0f) SwitchWeapon(-1);

        if (playerInput.SwitchToFirstWeapon) SwitchWeaponDirect(0);
        if (playerInput.SwitchToSecondWeapon) SwitchWeaponDirect(1);
        if (playerInput.SwitchToThirdWeapon) SwitchWeaponDirect(2);

        if (playerInput.SwitchToNextWeapon) SwitchWeapon(1);
        if (playerInput.SwitchToPreviousWeapon) SwitchWeapon(-1);
    }

    private void SwitchWeapon(int direction)
    {
        if (weapons.Count == 0 || isSwitchingWeapon) return;

        isSwitchingWeapon = true;
        switchCooldownTimer = switchCooldown;

        currentWeaponIndex += direction;

        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Count - 1;
        }

        EquipWeapon(currentWeaponIndex);
    }

    private void SwitchWeaponDirect(int index)
    {
        if (weapons.Count == 0 || isSwitchingWeapon) return;

        if (index < 0 || index >= weapons.Count) return;

        isSwitchingWeapon = true;
        switchCooldownTimer = switchCooldown;

        EquipWeapon(index);
    }

    private void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count) return;

        foreach (Transform child in weaponParent)
        {
            Destroy(child.gameObject);
        }

        currentWeapon = Instantiate(weapons[index], weaponParent).GetComponent<Weapon>();
        currentWeaponIndex = index;

        UpdateWeaponRotationToMouse(currentWeapon.gameObject);
    }

    private void UpdateWeaponRotationToMouse(GameObject gameObject)
    {
        if (currentWeapon == null) return;

        Vector3 mousePosition = playerInput.GetMousePosition();
        Vector3 direction = (mousePosition - weaponParent.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleSwitchCooldown()
    {
        if (isSwitchingWeapon)
        {
            switchCooldownTimer -= Time.deltaTime;
            if (switchCooldownTimer <= 0f)
            {
                isSwitchingWeapon = false;
            }
        }
    }

    public int GetWeaponIndex()
    {
        return currentWeaponIndex;
    }
}
