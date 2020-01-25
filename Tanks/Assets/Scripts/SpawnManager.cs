using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints => spawnPoints;
    public static List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float respawnTime = 3f;
    [SerializeField] Text timeTxt;
    [SerializeField] GameObject respawnPanel;

    public event Action Restart;

    private void Awake()
    {
        respawnPanel.SetActive(false);
    }

    public void RestartRound()
    {
        spawnedObjects.ForEach(s => Destroy(s));
        spawnedObjects.Clear();
        StartCoroutine(OnRestart());
    }

    IEnumerator OnRestart()
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
        Restart?.Invoke();
    }

    void RespawnPlayer()
    {
        GameManager.instance.NetworkManager.Player.Restart();
    }
}