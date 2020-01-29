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
            AddBulletToPool();
    }

    private void AddBulletToPool()
    {
        PhotonView bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", bulletPrefab.name), transform.position, Quaternion.identity).GetComponent<PhotonView>();
        bullet.RPC("Init", RpcTarget.AllBuffered, photonView.ViewID);
    }

    private void Update()
    {
        if (IsGunFarAwayFromWall() && photonView.IsMine && Input.GetMouseButtonDown(0))
            photonView.RPC(nameof(Fire), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Fire()
    {
        Bullet bullet = bulletsPool.Dequeue().GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.Shot(spawnPoint.position, -transform.up);
        // SpawnManager.spawnedObjects.Add(bullet);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
        bulletsPool.Enqueue(bullet.gameObject);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }

    public void IncreaseBullets()
    {
        maxBullets++;
        AddBulletToPool();
    }
}