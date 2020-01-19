using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints => spawnPoints;
    public static List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float respawnTime = 3f;
    [SerializeField] Text timeTxt;
    [SerializeField] GameObject respawnPanel;


    private void Awake()
    {
        respawnPanel.SetActive(false);
    }

    public void Restart()
    {
        spawnedObjects.ForEach(s => Destroy(s));
        spawnedObjects.Clear();
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        float leftTime = respawnTime;

        respawnPanel.SetActive(true);

        while (leftTime >= 0)
        {
            timeTxt.text = leftTime.ToString();
            --leftTime;
            yield return new WaitForSeconds(0.5f);
        }

        respawnPanel.SetActive(false);
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        GameManager.instance.NetworkManager.Player.Restart();
    }
}