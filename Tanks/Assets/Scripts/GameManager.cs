using Photon.Pun;
using UnityEngine;


public class GameManager : MonoBehaviourPun
{
    public static GameManager instance;

    [HideInInspector] public ScoreManager ScoreManager;
    [HideInInspector] public SpawnManager SpawnManager;
    [HideInInspector] public NetworkManager NetworkManager;


    private void Awake()
    {
        instance = this;

        SpawnManager = GetComponent<SpawnManager>();
        ScoreManager = GetComponent<ScoreManager>();
        NetworkManager = GetComponent<NetworkManager>();
    }

    public void Restart()
    {
        photonView.RPC("GoToNextRound", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void GoToNextRound()
    {
        ScoreManager.UpdateScore();
        SpawnManager.Respawn();
    }
}