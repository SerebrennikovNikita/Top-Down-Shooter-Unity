using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    private Vector2 movement;

    [SerializeField] private Transform torso;
    [SerializeField] private Animator legs;
    [SerializeField] private Camera aim;

    [SerializeField] private SpriteRenderer legsp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        Movement();
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        RotateBody();
        RotateBottom();
        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate()
    {
        if (movement.x != 0f || movement.y != 0f)
        {
            legs.SetBool("running", true);
        }
        else
        {
            legs.SetBool("running", false);
        }
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void RotateBody()
    {
        Vector2 direction = aim.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        torso.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void RotateBottom()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxis("Vertical");
        float angle = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg - 90f;
        legs.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (legs.transform.localEulerAngles.z > 90 && legs.transform.localEulerAngles.z < 270)
        {
            legs.transform.localScale = new Vector3(-1, -1, -1);
        }
        else
        {
            legs.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
