using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFlame : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    private Animator gun;
    [SerializeField] float speed;
    [SerializeField] float fireRate;
    float nextFire;
    private AudioSource shootSound;
    private void Awake()
    {
        gun = GetComponent<Animator>();
    }

    private void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Inventory.fuelCount > 0 && Pause.GameIsPaused == false)
            ShootBullet();
    }
    void ShootBullet()
    {
        if (Time.time > nextFire)
        {
            GameObject bullet = (Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);

            gun.SetTrigger("shoot");

            nextFire = Time.time + fireRate;
            Inventory.fuelCount--;
            shootSound.Play();
        }

    }
}
