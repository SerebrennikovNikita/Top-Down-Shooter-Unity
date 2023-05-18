using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDissapear : Dissapear
{
    private BoxCollider2D box;
    public override void Start()
    {
        box = GetComponent<BoxCollider2D>();
        Destroy(box, time / 3);
        Destroy(gameObject, time);
    }
}
