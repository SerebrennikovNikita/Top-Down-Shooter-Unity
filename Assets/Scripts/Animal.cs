using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
//using UnityEngine.WSA;

public class Animal : EnemyBasic
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shootRate;
    float nextShoot;

    public float radius = 4.5f;
    bool playerNear = false;
    bool monsterDead = false;
    bool safe = true;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public override void Update()
    {
        StartCoroutine(whait());

        IEnumerator whait()
        {
            yield return new WaitForSeconds(shootRate);
            safe = false;
        }
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < radius) 
        {
            playerNear = true;
        }
        if (distance > radius)
        {
            playerNear = false;
        }

        if (playerNear == true && monsterDead == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, -speed * 2 * Time.deltaTime);
        }

        if (playerNear == false)
        {
            ShootProjectile();
        }

        
        if (playerNear == false && monsterDead == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }

        RotateBody();
    }

    public override void RotateBody()
    {
        Vector2 direction = playerPosition.position - transform.position;
        if (playerNear == false)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if (playerNear == true)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 270f;
            rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void ShootProjectile()
    {
        if (Time.time > nextShoot && monsterDead == false && safe == false)
        {
            GameObject projectile = (Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            nextShoot = Time.time + shootRate;
            deathAnim.SetTrigger("shoot");
        }
    }

    public override void Die()
    {
        if (playerNear == false)
        {
            deathAnim.SetTrigger("death");
        }
        if (playerNear == true)
        {
            deathAnim.SetTrigger("retreat");
            Inventory.schore = Inventory.schore + 25;
        }
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 30f);
        sprite.sortingOrder--;
        monsterDead = true;
        Inventory.schore = Inventory.schore + schoreGain;
        deathSound.Play();
    }

}

