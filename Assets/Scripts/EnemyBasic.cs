using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public float speed;
    public int schoreGain;
    protected Transform playerPosition;
    protected Transform monsterPosition;
    protected Rigidbody2D rb;
    protected BoxCollider2D box;
    protected SpriteRenderer sprite;

    protected Animator deathAnim;
    public GameObject burnedPrefab;
    public GameObject gorePrefab;
    public AudioSource deathSound;
    public virtual void Start()
    {
        monsterPosition = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        deathAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    protected void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public virtual void Update()
    {
        RotateBody();
        //if (Vector2.Distance(transform.position, playerPosition.position) > 0.25f)
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
    }

    public virtual void RotateBody()
    {
        Vector2 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Explosion"))
            Gore();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
            Gore();
        if (collision.gameObject.CompareTag("Fire"))
            Burn();
    }
    public virtual void Die()
    {
        deathAnim.SetTrigger("death");
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 45f);
        sprite.sortingOrder--;
        Inventory.schore = Inventory.schore + schoreGain;
        deathSound.Play();
    }

    public virtual void Burn()
    {
        Destroy(gameObject);
        Inventory.schore = Inventory.schore + schoreGain;
        Instantiate(burnedPrefab, monsterPosition.position, monsterPosition.rotation);
    }

    public virtual void Gore()
    {
        Destroy(gameObject);
        Inventory.schore = Inventory.schore + schoreGain + 20;
        Instantiate(gorePrefab, monsterPosition.position, monsterPosition.rotation);
    }
}
