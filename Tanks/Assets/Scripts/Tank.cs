using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Tank : MonoBehaviourPun
{
    public Health Health { get; private set; }


    private void Awake()
    {
        Health = GetComponent<Health>();
        Health.Death += HandleDeath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Health.DoDamage(100);
    }

    private void HandleDeath()
    {
        PhotonNetwork.LocalPlayer.AddScore(-1);
        GameManager.instance.ScoreManager.UpdateScore();
        GameManager.instance.GoToNextRound();
    }
}