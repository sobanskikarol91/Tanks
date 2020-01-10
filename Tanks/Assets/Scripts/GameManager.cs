using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public ScoreManager score;
    [HideInInspector] public SpawnManager spawn;


    private void Awake()
    {
        instance = this;

        spawn = GetComponent<SpawnManager>();
        score = GetComponent<ScoreManager>();
    }
}