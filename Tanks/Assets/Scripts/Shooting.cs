using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;

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
        PrepareBulletPool();    
    }

    private void PrepareBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletsPool.Enqueue(bullet);
        }
    }

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
        GameObject bullet = bulletsPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = spawnPoint.position;
        SpawnManager.spawnedObjects.Add(bullet);
        bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, -transform.up, 0);
        AudioSource.PlayClipAtPoint(shotSnd, transform.position);
        bulletsPool.Enqueue(bullet);
    }

    bool IsGunFarAwayFromWall()
    {
        return !Physics2D.Raycast(spawnPoint.position, -transform.up, minDistanceToFire);
    }
}