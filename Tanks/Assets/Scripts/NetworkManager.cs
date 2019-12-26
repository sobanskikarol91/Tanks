using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections.Generic;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static int ConnectedPlayers { get; private set; } = 0;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        Debug.Log("Start");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Connected do server");
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
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NetworkPlayer"), Vector2.zero, Quaternion.identity);
        view.RPC("UpdatePlayersInfo", RpcTarget.All);
    }

    [PunRPC]
    void UpdatePlayersInfo()
    {
        ConnectedPlayers++;
    }
}