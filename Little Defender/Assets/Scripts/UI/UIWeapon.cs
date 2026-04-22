using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeapon : MonoBehaviour
{
    public List<GameObject> indicators;

    private PlayerWeapon playerWeapon;

    private void Awake()
    {
        playerWeapon = FindObjectOfType<PlayerWeapon>();
    }

    private void Update()
    {
        for (int i = 0; i < indicators.Count; i++)
        {
            indicators[i].SetActive(false);
            indicators[playerWeapon.GetWeaponIndex()].SetActive(true);
        }
    }
}
