using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static int ConnectedPlayers { get; set; } = 0;


    [SerializeField] Text serverInfo;
    public NetworkPlayer Player { get; private set; }

    private void Start()
    {
        Debug.Log("Start");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Connected do server: " + PhotonNetwork.CloudRegion);
        serverInfo.text = "Server: " + PhotonNetwork.CloudRegion; 
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room", options);
        Debug.Log("Creating room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Create room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Start game");
        Player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NetworkPlayer"), Vector2.zero, Quaternion.identity).GetComponent<NetworkPlayer>();
    }
}