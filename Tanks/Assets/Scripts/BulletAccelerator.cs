using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletAccelerator : MonoBehaviour
{
    [SerializeField] float multipler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet)
            bullet.IncreaseVelocity(multipler);
    }
}