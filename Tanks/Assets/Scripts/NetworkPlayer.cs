using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayer : MonoBehaviour 
{
    private void Start()
    {
        int nr = NetworkManager.ConnectedPlayers;

        GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player" +  nr), Vector2.zero, Quaternion.identity);
    }
}