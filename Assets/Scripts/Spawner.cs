using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnPoint;

    public float safe;

    public float spawnRate;
    protected float nextSpawn;

    void Update()
    {
        if (Inventory.timer >= safe)
        {
            SpawnMonster();
        }
    }
    public virtual void SpawnMonster()
    {
        if (Time.time > nextSpawn)
        {
            GameObject monster = (Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation));

            nextSpawn = Time.time + spawnRate;
        }
    }
}
