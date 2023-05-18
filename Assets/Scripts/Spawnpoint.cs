using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform spawnPoint;
    void Start()
    {
        Destroy(gameObject, 1.2f);
    }

    void Spawn()
    {
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
