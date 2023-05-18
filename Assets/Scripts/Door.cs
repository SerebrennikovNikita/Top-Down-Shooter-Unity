using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float locked;
    [SerializeField] private TMP_Text schoreText;
    void Update()
    {
        schoreText.text = "" + locked;
        if (Inventory.schore >= locked)
        {
            Destroy(gameObject);
        }
    }
}
