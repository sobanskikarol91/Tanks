using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayer : MonoBehaviour
{
    private PhotonView view;
    GameObject avatar;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
            CreateAvatar();
    }

    private void CreateAvatar()
    {
        int nr = NetworkManager.ConnectedPlayers;
        avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player" + nr), Vector2.zero, Quaternion.identity);
        view.RPC("UpdatePlayersInfo", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void UpdatePlayersInfo()
    {
        NetworkManager.ConnectedPlayers++;
        Debug.Log(NetworkManager.ConnectedPlayers);
    }
}