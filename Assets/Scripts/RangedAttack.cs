using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 10;
    public float fireRate = 1;
    public float projectileLifetime = 2;
    public float projectileDamage = 1;
    public float projectileKnockbackTime = 1;
    public float projectileSize = 1;
    public Vector2 projectileDirection;
    private bool canFire = true;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    void Update()
    {
        checkDirection();
        if (playerMovement.powerUps.Contains(powerUp.gun) && canFire && Input.GetButtonDown("Fire1") && playerMovement.currentState != PlayerState.stunned)
        {
            Fire();
            StartCoroutine(FireCooldown());
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileSpeed;
        bullet.GetComponent<Projectile>().projectileDamage = projectileDamage;
        bullet.GetComponent<Projectile>().projectileKnockbackTime = projectileKnockbackTime;
        bullet.GetComponent<Projectile>().projectileSize = projectileSize;
        Destroy(bullet, projectileLifetime);
    }

    public void checkDirection()
    {
        if (playerMovement.direction == PlayerDirection.up)
        {
            projectileDirection = new Vector2(0, 1);
        }
        else if (playerMovement.direction == PlayerDirection.down)
        {
            projectileDirection = new Vector2(0, -1);
        }
        else if (playerMovement.direction == PlayerDirection.left)
        {
            projectileDirection = new Vector2(-1, 0);
        }
        else if (playerMovement.direction == PlayerDirection.right)
        {
            projectileDirection = new Vector2(1, 0);
        }

    }
}
