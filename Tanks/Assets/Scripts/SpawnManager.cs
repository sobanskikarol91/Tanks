using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints => spawnPoints;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float respawnTime = 3f;
    
    public void Respawn()
    {
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
    }

}