using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBackub : MonoBehaviour
{
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] float speed;
    private Transform playerPosition;
    private Transform monsterPosition;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sprite;

    private Animator deathAnim;

    private void Start()
    {
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
        Vector2 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.transform.rotation = Quaternion.Euler(0, 0, angle);
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
        Destroy(rb);
        Destroy(box);
        Destroy(gameObject, 10f);
        sprite.sortingOrder--;

        GameObject shell = (Instantiate(bloodPrefab, monsterPosition.position, monsterPosition.rotation));
        Rigidbody2D rbshell = shell.GetComponent<Rigidbody2D>();
        rbshell.AddForce(monsterPosition.up * -10f, ForceMode2D.Impulse);
        Destroy(rbshell, 0.3f);
    }
}
