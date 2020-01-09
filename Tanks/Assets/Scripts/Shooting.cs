using UnityEngine;
using Photon.Pun;
using System.IO;

public class Shooting : MonoBehaviourPun
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;

    private void Update()
    {
        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Rocket"), spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Bullet>().InitializeBullet(-transform.up, 0);
        }
    }
}