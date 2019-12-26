using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks 
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Start game");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NetworkPlayer"), Vector2.zero, Quaternion.identity);
    }
}