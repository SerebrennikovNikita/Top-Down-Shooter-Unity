using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static int ammoCount = 25;
    public static int fuelCount = 5;
    public static int grenadeCount = 1;
    public static bool haveFlametrower = false;
    public static bool haveRocketLauncher = false;

    public static int schore = 0;
    public static int schorePrevious = 0;
    public static int schoreBest = 0;
    public static float timer;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text grenadeText;
    [SerializeField] private TMP_Text schoreText;
    [SerializeField] private TMP_Text schoreBestText;
    [SerializeField] private AudioSource picUpSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            Destroy(collision.gameObject);
            ammoCount = ammoCount + 10;
            schore = schore + 10;
            picUpSound.Play();
        }
        if (collision.gameObject.CompareTag("Fuel"))
        {
            Destroy(collision.gameObject);
            fuelCount = fuelCount + 3;
            schore = schore + 15;
            picUpSound.Play();
        }
        if (collision.gameObject.CompareTag("Grenade"))
        {
            Destroy(collision.gameObject);
            grenadeCount = grenadeCount + 2;
            schore = schore + 25;
            picUpSound.Play();
        }
        if (collision.gameObject.CompareTag("WeaponF"))
        {
            Destroy(collision.gameObject);
            fuelCount = fuelCount + 5;
            haveFlametrower = true;
            schore = schore + 100;
            picUpSound.Play();
        }
        if (collision.gameObject.CompareTag("WeaponR"))
        {
            Destroy(collision.gameObject);
            grenadeCount = grenadeCount + 2;
            haveRocketLauncher = true;
            schore = schore + 200;
            picUpSound.Play();
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (ammoCount > 60)
        {
            ammoCount = 60;
        }
        if (fuelCount > 30)
        {
            fuelCount = 30;
        }
        if (grenadeCount > 20)
        {
            grenadeCount = 20;
        }
        timerText.text = "" + timer;
        ammoText.text = "" + ammoCount;
        fuelText.text = "" + fuelCount;
        grenadeText.text = "" + grenadeCount;
        schoreText.text = "schore: " + schore;
        schoreBestText.text = "BEST: " + schoreBest;
    }
}
