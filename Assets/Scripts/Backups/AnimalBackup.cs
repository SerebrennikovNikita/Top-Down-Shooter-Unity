using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBackup : MonoBehaviour
{
    [SerializeField] float speed;
    private Transform playerPosition;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sprite;

    private Animator monsterAnim;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shootRate;
    float nextShoot;
    bool stopShooting = false;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        monsterAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        ShootProjectile();
        RotateBody();
        if (Vector2.Distance(transform.position, playerPosition.position) > 0.25f)
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
    }

    void RotateBody()
    {
        Vector2 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion"))
        {
            Die();
        }
    }
    void Die()
    {
        monsterAnim.SetTrigger("death");
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 10f);
        sprite.sortingOrder--;
        stopShooting = true;
    }
    void ShootProjectile()
    {
        if (Time.time > nextShoot && stopShooting == false)
        {
            GameObject projectile = (Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextShoot = Time.time + shootRate;
            monsterAnim.SetTrigger("shoot");
        }
    }

}
