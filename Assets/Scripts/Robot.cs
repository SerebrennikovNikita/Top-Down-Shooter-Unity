using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : EnemyBasic
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shootRate;
    float nextShoot;

    public float radius = 8f;
    bool playerNear = false;
    bool monsterDead = false;
    int health = 1;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public override void Update()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < radius)
        {
            playerNear = true;
            speed = 0;
        }
        if (distance > radius)
        {
            playerNear = false;
            speed = 8.5f;
        }

        if (playerNear == true)
        {
            ShootProjectile();
        }

        if (Vector2.Distance(transform.position, playerPosition.position) > 0.25f && playerNear == false && monsterDead == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }
        RotateBody();
    }

    void ShootProjectile()
    {
        if (Time.time > nextShoot && monsterDead == false)
        {
            GameObject projectile = (Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextShoot = Time.time + shootRate;
            deathAnim.SetTrigger("shoot");
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion"))
        {
            
            if (health <= 0)
            {
                Die();
            }
            health--;
            Instantiate(bloodPrefab, monsterPosition.position, monsterPosition.rotation);
            deathAnim.SetBool("weak", true);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
            Gore();
        if (collision.gameObject.CompareTag("Fire") && health <= 0)
            Burn();
    }

    public override void Die()
    {
        deathAnim.SetTrigger("death");
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 30f);
        sprite.sortingOrder--;
        monsterDead = true;
        Inventory.schore = Inventory.schore + schoreGain;
        deathSound.Play();
    }
    public override void Burn()
    {
        Instantiate(explosionPrefab, monsterPosition.position, monsterPosition.rotation);
    }
}
