using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
//using UnityEngine.WSA;

public class Monster : EnemyBasic
{
    [SerializeField] GameObject bloodPrefab;
    public override void Die()
    {
        deathAnim.SetTrigger("death");
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 30f);
        sprite.sortingOrder--;

        GameObject blood = (Instantiate(bloodPrefab, monsterPosition.position, monsterPosition.rotation));
        Rigidbody2D rbblood = blood.GetComponent<Rigidbody2D>();
        rbblood.AddForce(monsterPosition.up * -10f, ForceMode2D.Impulse);
        Destroy(rbblood, 0.3f);
        Inventory.schore = Inventory.schore + schoreGain;
        deathSound.Play();
    }
}
