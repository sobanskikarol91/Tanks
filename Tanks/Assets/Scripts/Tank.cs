using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Tank : MonoBehaviourPun
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.Death += HandleDeath;
    }

    private void HandleDeath()
    {
        PhotonNetwork.LocalPlayer.AddScore(-1);
        GameManager.instance.score.UpdateScore();
        GameManager.instance.spawn.Respawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            health.DoDamage(10);
    }
}