using System;
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
            ClientInit();
    }

    private void ClientInit()
    {
        Health.Death += HandleDeath;

        Renderer[] renderer = gameObject.GetComponentsInChildren<Renderer>();
        Array.ForEach(renderer, r => r.sortingLayerName = "Foreground");
        gameObject.GetComponentInChildren<Canvas>().sortingLayerName = "Foreground";
        gameObject.AddComponent<CameraFollower>();
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