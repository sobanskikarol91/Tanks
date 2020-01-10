using UnityEngine;


public class GameManager : MonoBehaviour
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

    public void GoToNextRound()
    {
        SpawnManager.Respawn();
    }
}