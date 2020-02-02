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

    private float maxBullets = 6;
    private float startBullet = 2;
    private float minDistanceToFire = 0.2f;


    public Queue<GameObject> bulletsPool = new Queue<GameObject>();
    public Queue<GameObject> currentBullets = new Queue<GameObject>();

    private void Awake()
    {
        if (photonView.IsMine)
            PrepareBullets();
    }


    private void PrepareBullets()
    {
        for (int i = 0; i < maxBullets; i++)
            AddBulletToPool();

        photonView.RPC(nameof(PrepareCurrentPool), RpcTarget.All);
    }

    private void AddBulletToPool()
    {
        PhotonView bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", bulletPrefab.name), transform.position, Quaternion.identity).GetComponent<PhotonView>();
        bullet.RPC("Init", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    private void PrepareCurrentPool()
    {
        for (int i = 0; i < startBullet; i++)
            IncreaseBullets();
    }

    private void Update()
    {
        if (IsGunFarAwayFromWall() && photonView.IsMine && Input.GetMouseButtonDown(0))
            photonView.RPC(nameof(Fire), RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Fire()
    {
        Bullet bullet = currentBullets.Dequeue().GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.Shot(spawnPoint.position, -transform.up);
        // SpawnManager.spawnedObjects.Add(bullet);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
        currentBullets.Enqueue(bullet.gameObject);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }

    [PunRPC]
    public void IncreaseBullets()
    {
        if (bulletsPool.Count == 0)
            Debug.Log("bullet pool is empty");
        else
            currentBullets.Enqueue(bulletsPool.Dequeue());
    }
}