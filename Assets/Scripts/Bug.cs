using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : EnemyBasic
{
    [SerializeField] GameObject explosionPrefab;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
            Die();
        if (collision.gameObject.CompareTag("Fire"))
            Burn();
    }
    public override void Die()
    {
        Destroy(gameObject);
        Instantiate(gorePrefab, monsterPosition.position, monsterPosition.rotation);
        Instantiate(explosionPrefab, monsterPosition.position, monsterPosition.rotation);
        Inventory.schore = Inventory.schore + schoreGain;
    }
}
