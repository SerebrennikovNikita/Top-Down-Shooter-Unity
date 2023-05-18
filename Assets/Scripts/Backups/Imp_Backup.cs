using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Imp_Backup : MonoBehaviour
{
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] float speed;
    private Transform playerPosition;
    private Transform monsterPosition;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sprite;

    private Animator deathAnim;
    bool Dead = false;
    SortingGroup sortingGroup;

    private void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        monsterPosition = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        deathAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        RotateBody();
        if (Vector2.Distance(transform.position, playerPosition.position) > 0.25f)
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
    }

    void RotateBody()
    {
        if (Dead == false)
        {
            Vector2 direction = playerPosition.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion"))
        {
            Die();
        }
    }
    void Die()
    {
        deathAnim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Dynamic;
        speed = 0;
        Destroy(box);
        Destroy(gameObject, 10f);
        sprite.sortingOrder--;
        rb.AddForce(rb.transform.up * -11f, ForceMode2D.Impulse);
        Destroy(rb, 1f);
        Dead = true;
        Destroy(sortingGroup);

        Instantiate(bloodPrefab, monsterPosition.position, monsterPosition.rotation);
    }
}
