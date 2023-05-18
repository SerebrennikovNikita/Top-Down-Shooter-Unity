using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.WSA;

public class Imp : EnemyBasic
{
    [SerializeField] GameObject bloodPrefab;

    bool Dead = false;
    SortingGroup sortingGroup;

    public override void RotateBody()
    {
        if (Dead == false)
        {
            Vector2 direction = playerPosition.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        }  
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Explosion"))
            Gore();
        if (collision.gameObject.CompareTag("Fire"))
            Burn();
    }
    public override void Die()
    {
        sortingGroup = GetComponent<SortingGroup>();
        deathAnim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Dynamic;
        speed = 0;
        Destroy(box);
        Destroy(gameObject, 30f);
        sprite.sortingOrder--;
        rb.AddForce(rb.transform.up * -11f, ForceMode2D.Impulse);
        Destroy(rb, 1f);
        Dead = true;
        Destroy(sortingGroup);

        Instantiate(bloodPrefab, monsterPosition.position, monsterPosition.rotation);

        Inventory.schore = Inventory.schore + schoreGain;
        deathSound.Play();
    }
}
