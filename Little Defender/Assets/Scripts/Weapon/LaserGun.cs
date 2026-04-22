using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    protected override void Shoot()
    {
        AudioManager.Instance.Play("LaserGun_Shoot");

        Projectile projectile = Instantiate(projectilePrefab, projectileFirePoint.position, projectileFirePoint.rotation);
        projectile.Initialize(transform.right, projectileSpeed, projectileDamage, projectileLifetime);
    }
}
