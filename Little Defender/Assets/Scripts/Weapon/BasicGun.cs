using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Weapon
{
    protected override void Shoot()
    {
        AudioManager.Instance.Play("BasicGun_Shoot");

        Projectile projectile = Instantiate(projectilePrefab, projectileFirePoint.position, projectileFirePoint.rotation);
        projectile.Initialize(transform.right, projectileSpeed, projectileDamage, projectileLifetime);
    }
}
