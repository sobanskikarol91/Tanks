using UnityEngine;
using Photon.Pun;


public class Shooting : MonoBehaviourPun
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shotSnd;


    private void Update()
    {
        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            photonView.RPC("Fire", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, -transform.up, 0);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
    }
}