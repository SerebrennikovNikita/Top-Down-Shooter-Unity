using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private BoxCollider2D box;
    [SerializeField] GameObject explosionPrefab;
    private Transform barrelPosition;

    void Start()
    {
        barrelPosition = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Fire"))
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Fire"))
            Die();
       
    }
    void Die()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, barrelPosition.position, barrelPosition.rotation);
        Inventory.schore = Inventory.schore + 10;
    }
}
