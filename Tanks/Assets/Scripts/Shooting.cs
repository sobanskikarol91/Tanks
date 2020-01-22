using UnityEngine;
using Photon.Pun;


public class Shooting : MonoBehaviourPun
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shotSnd;

    private float minDistanceToFire = 0.2f;


    private void Update()
    {
        if (IsGunFarAwayFromWall() && photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            photonView.RPC("Fire", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        SpawnManager.spawnedObjects.Add(bullet);
        bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, -transform.up, 0);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }
}