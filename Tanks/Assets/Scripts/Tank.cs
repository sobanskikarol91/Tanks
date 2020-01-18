﻿using System;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Tank : MonoBehaviourPun
{
    public Health Health { get; private set; }


    private void Awake()
    {
        Health = GetComponent<Health>();

        if (photonView.IsMine)
            Health.Death += HandleDeath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Health.DoDamage(10);
    }

    private void HandleDeath()
    {
        if (photonView.IsMine)
            PhotonNetwork.LocalPlayer.AddScore(-1);

        GameManager.instance.Restart();
    }
}