using System;
using Photon.Pun;
using UnityEngine;


public class GameManager : MonoBehaviourPun, IRestart
{
    public static GameManager instance;
    public GameSettings settings;

    [HideInInspector] public ScoreManager ScoreManager;
    [HideInInspector] public SpawnManager SpawnManager;
    [HideInInspector] public NetworkManager NetworkManager;
    [HideInInspector] public EnviromentManager EnviromentManager;
    

    private void Awake()
    {
        instance = this;

        SpawnManager = GetComponent<SpawnManager>();
        ScoreManager = GetComponent<ScoreManager>();
        NetworkManager = GetComponent<NetworkManager>();
        EnviromentManager = GetComponent<EnviromentManager>();
    }

    private void Start()
    {
        SpawnManager.Restart += AfterRestart;
    }

    private void AfterRestart()
    {
        EnviromentManager.Restart();
    }

    public void Restart()
    {
        photonView.RPC(nameof(GoToNextRound), RpcTarget.AllViaServer);
    }

    [PunRPC]
    void GoToNextRound()
    {
        ScoreManager.UpdateScore();
        SpawnManager.RestartRound();
    }
}