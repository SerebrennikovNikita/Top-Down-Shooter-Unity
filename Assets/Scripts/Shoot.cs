using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shellPrefab;
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
        if (Input.GetButton("Fire1") && Inventory.ammoCount > 0 && Pause.GameIsPaused == false)
            ShootBullet();
    }
    void ShootBullet()
    {
        if (Time.time > nextFire)
        {
            GameObject bullet = (Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);

            GameObject shell = (Instantiate(shellPrefab, firePoint.position, firePoint.rotation));
            Rigidbody2D rbshell = shell.GetComponent<Rigidbody2D>();
            rbshell.AddForce(firePoint.up * -15f, ForceMode2D.Impulse);
            Destroy(rbshell, 0.15f);

            gun.SetTrigger("shoot");

            nextFire = Time.time + fireRate;
            Inventory.ammoCount--;
            shootSound.Play();
        }
        
    }
}
