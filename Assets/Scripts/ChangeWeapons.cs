using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapons : MonoBehaviour
{
    int totalWeapon = 1;
    public int currentWeaponIndex;
    public GameObject[] weapons;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    void Start()
    {
        totalWeapon = weaponHolder.transform.childCount;

        weapons = new GameObject[totalWeapon];
        for (int i = 0; i < totalWeapon; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }

        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
        currentWeapon = weapons[currentWeaponIndex];
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3) && Inventory.haveRocketLauncher == true)
        {

            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = 2;
            weapons[currentWeaponIndex].SetActive(true);
            currentWeapon = weapons[currentWeaponIndex];

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Inventory.haveFlametrower == true)
        {
            
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 1;
                weapons[currentWeaponIndex].SetActive(true);
                currentWeapon = weapons[currentWeaponIndex];
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 0;
                weapons[currentWeaponIndex].SetActive(true);
                currentWeapon = weapons[currentWeaponIndex];
            
        }
    }
}
