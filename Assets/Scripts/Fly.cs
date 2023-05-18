using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject puffPrefab;
    private Transform playerPosition;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Transform bulletPosition;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        bulletPosition = GetComponent<Transform>();
        Destroy(gameObject, 2.5f);
    }
    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        RotateBody();
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
        Destroy(gameObject);
        Instantiate(puffPrefab, bulletPosition.position, bulletPosition.rotation);
    }
}
