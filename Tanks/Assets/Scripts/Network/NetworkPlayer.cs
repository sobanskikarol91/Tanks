using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayer : MonoBehaviour
{
    private Tank avatar;
    private PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
            CreateAvatar();
    }

    private void CreateAvatar()
    {
        int nr = NetworkManager.ConnectedPlayers;
        Transform spawnPoint = GameManager.instance.SpawnManager.SpawnPoints[nr].transform;

        avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player" + nr), spawnPoint.position, spawnPoint.rotation).GetComponent<Tank>();
        view.RPC("UpdatePlayersInfo", RpcTarget.AllBuffered);
    }

    public void RestartAvatar()
    {
        avatar.gameObject.SetActive(true);
        //avatar.
    }

    [PunRPC]
    void UpdatePlayersInfo()
    {
        NetworkManager.ConnectedPlayers++;
    }
}