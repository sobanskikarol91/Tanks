using UnityEngine;
using Photon.Pun;
using System.IO;


public class NetworkPlayer : MonoBehaviourPun, IRestart
{
    private Tank avatar;
    private int nr;
    [SerializeField] GameObject[] tanksPrefabs;

    private void Start()
    {
        if (photonView.IsMine)
            AddPlayerToGame();
    }

    private void AddPlayerToGame()
    {
        nr = NetworkManager.ConnectedPlayers;
        CreateAvatar();
        PhotonNetwork.LocalPlayer.NickName = "Player " + nr;
        photonView.RPC(nameof(UpdatePlayersInfo), RpcTarget.AllBuffered);
    }

    private void CreateAvatar()
    {
        Debug.Log("Create avatar: " + NetworkManager.ConnectedPlayers);
        Transform spawnPoint = GameManager.instance.SpawnManager.SpawnPoints[nr].transform;
        avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", tanksPrefabs[nr].name), spawnPoint.position, spawnPoint.rotation).GetComponent<Tank>();
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