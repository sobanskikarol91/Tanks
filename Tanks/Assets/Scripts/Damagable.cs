﻿using Photon.Pun;
using UnityEngine;


public class Damagable : MonoBehaviourPun
{
    [SerializeField] float damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionDetected(collision);
    }

    private void CollisionDetected(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            DoDamage(collision);
    }

    private void DoDamage(Collision2D collision)
    {
        if (photonView.IsMine)
        {
            Health health = collision.gameObject.GetComponent<Health>();

            if (health)
                health.photonView.RPC(nameof(health.DoDamage), RpcTarget.AllBuffered, damage);

            photonView.RPC(nameof(HideObject), RpcTarget.All);
        }

    }

    [PunRPC]
    void HideObject()
    {
        gameObject.SetActive(false);
    }
}