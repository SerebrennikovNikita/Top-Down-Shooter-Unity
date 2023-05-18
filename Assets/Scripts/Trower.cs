using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trower : Spawner
{
    private Animator Anim;

    [SerializeField] float destroyBarrelRBTime = 0.5f;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public override void SpawnMonster()
    {
        if (Time.time > nextSpawn)
        {
            Anim.SetTrigger("shoot");
            nextSpawn = Time.time + spawnRate;
        }
    }

    void Shoot()
    {
        GameObject blood = (Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation));
        Rigidbody2D rbblood = blood.GetComponent<Rigidbody2D>();
        rbblood.AddForce(spawnPoint.up * 9f, ForceMode2D.Impulse);
        
        Destroy(rbblood, destroyBarrelRBTime);

        StartCoroutine(whait());

        IEnumerator whait()
        {
            yield return new WaitForSeconds(destroyBarrelRBTime);
            blood.tag = "Untagged";
        }
        
            
        
    }
}
