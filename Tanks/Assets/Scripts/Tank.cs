using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Tank : MonoBehaviourPun
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.Death += UpdateScore;
    }

    private void UpdateScore()
    {
        PhotonNetwork.LocalPlayer.AddScore(-1);
        GameManager.instance.score.UpdateScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            health.DoDamage(10);
    }
}