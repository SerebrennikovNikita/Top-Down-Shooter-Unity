using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Projectile"))
        RestartLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Explosion"))
            RestartLevel();
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Inventory.ammoCount = 25;
        Inventory.fuelCount = 5;
        Inventory.grenadeCount = 1;
        Inventory.timer = 0;
        Inventory.haveFlametrower = false;
        Inventory.haveRocketLauncher = false;
        Inventory.schorePrevious = Inventory.schore;

        if (Inventory.schoreBest < Inventory.schorePrevious)
        {
            Inventory.schoreBest = Inventory.schorePrevious;
        }

        Inventory.schore = 0;

    }
}
