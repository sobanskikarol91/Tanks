using Photon.Pun;
using UnityEngine;


public class GameManager : MonoBehaviourPun, IRestart
{
    public static GameManager instance;

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

    public void Restart()
    {
        photonView.RPC("GoToNextRound", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void GoToNextRound()
    {
        EnviromentManager.Restart();
        ScoreManager.UpdateScore();
        SpawnManager.Restart();
    }
}