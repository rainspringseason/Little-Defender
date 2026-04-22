using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool Shoot { get; private set; }
    public bool SwitchToFirstWeapon { get; private set; }
    public bool SwitchToSecondWeapon { get; private set; }
    public bool SwitchToThirdWeapon { get; private set; }
    public bool SwitchToNextWeapon { get; private set; }
    public bool SwitchToPreviousWeapon { get; private set; }
    public float ScrollMouse { get; private set; }

    [Header("Move Keys")]
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    [Header("Shoot Keys")]
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("Switch Weapon")]
    public KeyCode firstWeapon = KeyCode.Alpha1;
    public KeyCode secondWeapon = KeyCode.Alpha2;
    public KeyCode thirdWeapon = KeyCode.Alpha3;

    public KeyCode nextWeapon = KeyCode.E;
    public KeyCode previousWeapon = KeyCode.Q;

    private void Update()
    {
        if (!GameManager.Instance.GetPause())
        {
            MoveInput();
            ShootInput();
            WeaponSwitchInput();
        }
    }

    private void MoveInput()
    {
        float XInput = 0f;
        float YInput = 0f;

        if (Input.GetKey(moveUpKey)) YInput++;
        if (Input.GetKey(moveDownKey)) YInput--;
        if (Input.GetKey(moveLeftKey)) XInput--;
        if (Input.GetKey(moveRightKey)) XInput++;

        Move = new Vector2(XInput, YInput).normalized;
    }

    private void ShootInput()
    {
        Shoot = Input.GetKey(shootKey);
    }

    private void WeaponSwitchInput()
    {
        ScrollMouse = Input.GetAxis("Mouse ScrollWheel");

        SwitchToFirstWeapon = Input.GetKeyDown(firstWeapon);
        SwitchToSecondWeapon = Input.GetKeyDown(secondWeapon);
        SwitchToThirdWeapon = Input.GetKeyDown(thirdWeapon);

        SwitchToNextWeapon = Input.GetKeyDown(nextWeapon);
        SwitchToPreviousWeapon = Input.GetKeyDown(previousWeapon);
    }

    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
