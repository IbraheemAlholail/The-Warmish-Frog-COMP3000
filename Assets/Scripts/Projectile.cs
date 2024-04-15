using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float fireRate;
    public float projectileLifetime;
    public float projectileDamage;
    public float projectileKnockbackTime;
    public float projectileSize;
    public float projectileDirection;

    public Rigidbody2D rb;
    public Vector2 direction;
    public BoxCollider2D boxCollider;

    public PlayerMovement playerMovement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale = new Vector3(projectileSize, projectileSize, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemies"))
        {

            collision.gameObject.GetComponent<Enemy>().Knock(collision.GetComponent<Rigidbody2D>(), projectileKnockbackTime, projectileDamage);
            Destroy(gameObject);
        }
    }
}
