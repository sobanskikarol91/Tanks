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
           // GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Rocket"), spawnPoint.position, spawnPoint.rotation);
            photonView.RPC("Fire", RpcTarget.AllViaServer, spawnPoint.position, spawnPoint.rotation);
        }
    }

    [PunRPC]
    private void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        bullet.GetComponent<Bullet>().InitializeBullet(-transform.up, 0);
    }
}