using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints => spawnPoints;
    [SerializeField] Transform[] spawnPoints;
}