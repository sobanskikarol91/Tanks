﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

using static Photon.Pun.PhotonNetwork;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text[] scores;

    [PunRPC]
    public void UpdateScore()
    {
        for (int i = 0; i < PlayerList.Length; i++)
        {
            scores[i].text = PlayerList[i].GetScore().ToString();
        }
    }
}