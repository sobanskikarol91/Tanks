using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Random = UnityEngine.Random;
using System.IO;

public class WeaponSpawner : MonoBehaviour, IRestart
{
    [SerializeField] GameObject[] powerUps;
    [SerializeField] float spawnTime;

    public void Init()
    {
        StartCoroutine(Spawn());
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            int index = Random.Range(0, powerUps.Length);
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", powerUps[index].name), transform.position, Quaternion.identity);
        }
    }
}