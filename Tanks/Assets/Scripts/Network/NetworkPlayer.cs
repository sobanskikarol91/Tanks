using UnityEngine;
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
        int nr = NetworkManager.ConnectedPlayers;
        CreateAvatar();

        photonView.RPC("UpdatePlayersInfo", RpcTarget.AllBufferedViaServer);
    }

    private void CreateAvatar()
    {
        Transform spawnPoint = GameManager.instance.SpawnManager.SpawnPoints[nr].transform;
        avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player" + nr), spawnPoint.position, spawnPoint.rotation).GetComponent<Tank>();
    }

    [PunRPC]
    void UpdatePlayersInfo()
    {
        NetworkManager.ConnectedPlayers++;
    }

    public void Restart()
    {
        if (avatar != null)
            PhotonNetwork.Destroy(avatar.gameObject);

        CreateAvatar();
    }
}