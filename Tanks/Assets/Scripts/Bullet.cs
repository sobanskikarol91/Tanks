﻿using UnityEngine;
using Photon.Realtime;


public class Bullet : MonoBehaviour
{
    public Player Owner { get; private set; }
    [SerializeField] float speed = 10;
    private new Rigidbody2D rigidbody;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetRotationAccordingToMovement();
    }

    private void SetRotationAccordingToMovement()
    {
        float angle = 360 - Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void InitializeBullet(Player owner, Vector3 direction, float lag)
    {
        Owner = owner;
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }
}