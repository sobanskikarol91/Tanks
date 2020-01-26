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
    public Queue<GameObject> bulletsPool = new Queue<GameObject>();


    private void Awake()
    {
        if (photonView.IsMine)
            PrepareBulletPool();
    }

    private void PrepareBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            PhotonView bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", bulletPrefab.name), transform.position, Quaternion.identity).GetComponent<PhotonView>();
            bullet.RPC("Init", RpcTarget.AllBuffered, photonView.ViewID);
        }
    }

    private void Update()
    {
        if (IsGunFarAwayFromWall() && photonView.IsMine && Input.GetMouseButtonDown(0))
            photonView.RPC(nameof(Fire), RpcTarget.All);
    }

    [PunRPC]
    private void Fire()
    {
        Bullet bullet = bulletsPool.Dequeue().GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = spawnPoint.position;
        // SpawnManager.spawnedObjects.Add(bullet);
        //bullet.GetComponent<Bullet>().Init(photonView.Owner, -transform.up, 0);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
        bullet.Shot(spawnPoint.transform.position, -transform.up);
        bulletsPool.Enqueue(bullet.gameObject);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }
}