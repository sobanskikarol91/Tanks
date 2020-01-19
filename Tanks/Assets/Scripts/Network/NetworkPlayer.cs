﻿using UnityEngine;
using Photon.Pun;
using System.IO;
using System;

public class NetworkPlayer : MonoBehaviourPun, IRestart
{
    private Tank avatar;
    private int nr;


    private void Start()
    {
        if (photonView.IsMine)
            AddPlayerToGame();
    }

    private void AddPlayerToGame()
    {
        nr = NetworkManager.ConnectedPlayers;
        CreateAvatar();

        photonView.RPC("UpdatePlayersInfo", RpcTarget.AllBuffered);
    }

    private void CreateAvatar()
    {
        Debug.Log("Create avatar: " + NetworkManager.ConnectedPlayers);
        Transform spawnPoint = GameManager.instance.SpawnManager.SpawnPoints[nr].transform;
        avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Tank" + nr), spawnPoint.position, spawnPoint.rotation).GetComponent<Tank>();
    }

    [PunRPC]
    void UpdatePlayersInfo()
    {
        NetworkManager.ConnectedPlayers++;
        Debug.Log("ConnectedPlayers:" + NetworkManager.ConnectedPlayers);
    }

    public void Restart()
    {
        if (avatar != null)
            PhotonNetwork.Destroy(avatar.gameObject);

        CreateAvatar();
    }
}