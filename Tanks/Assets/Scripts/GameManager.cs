using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager spawnManager;

    private void Awake()
    {
        instance = this;
        spawnManager = GetComponent<SpawnManager>();
    }
}