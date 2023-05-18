using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject puffPrefab;
    protected Transform bulletPosition;
    public float timer;
    public virtual void Start()
    {
        bulletPosition = GetComponent<Transform>();
        Destroy(gameObject, timer);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(puffPrefab, bulletPosition.position, bulletPosition.rotation);
    }
}
