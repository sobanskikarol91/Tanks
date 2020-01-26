using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;
using System.IO;

public class Shooting : MonoBehaviourPun
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shotSnd;
    [SerializeField] float maxBullets = 3;

    private float minDistanceToFire = 0.2f;
    private Queue<GameObject> bulletsPool = new Queue<GameObject>();


    private void Awake()
    {
        //if (photonView.IsMine)
        //    PrepareBulletPool();
    }

    private void PrepareBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", bulletPrefab.name), transform.position, Quaternion.identity);
            bulletsPool.Enqueue(bullet);
        }
    }

    private void Update()
    {
        if (IsGunFarAwayFromWall() && photonView.IsMine && Input.GetMouseButtonDown(0))
            Fire();
    }

    private void Fire()
    {
        //GameObject bullet = bulletsPool.Dequeue();
        //bullet.SetActive(true);
        //bullet.transform.position = spawnPoint.position;
        //SpawnManager.spawnedObjects.Add(bullet);
        //bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, -transform.up, 0);
        //AudioSource.PlayClipAtPoint(shotSnd, transform.position);
        //bulletsPool.Enqueue(bullet);

        PhotonView bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", bulletPrefab.name), transform.position, Quaternion.identity).GetComponent<PhotonView>();

        bullet.RPC("Shot", RpcTarget.All, spawnPoint.position);
        bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, -transform.up, 0);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }
}