using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Damagable : MonoBehaviour
{
    [SerializeField] float damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
            health.DoDamage(damage);
    }
}