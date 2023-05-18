using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
    public float time = 3;
    public virtual void Start()
    {
        Destroy(gameObject, time);
    }
}
