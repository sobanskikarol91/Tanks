using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public SpawnManager spawnManager;

    private void Awake()
    {
        instance = this;
        spawnManager = GetComponent<SpawnManager>();
    }
}