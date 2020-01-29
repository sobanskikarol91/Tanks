using UnityEngine;
using Photon.Pun;
using System;

public class Bullet : MonoBehaviourPun, IRestart
{
    [SerializeField] float speed = 10;
    private new Rigidbody2D rigidbody;


    private void Awake()
    {
        SpawnManager.spawnedObjects.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        SpawnManager.spawnedObjects.Remove(this);
    }

    private void FixedUpdate()
    {
        SetRotationAccordingToMovement();
    }

    private void SetRotationAccordingToMovement()
    {
        float angle = 360 - Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    [PunRPC]
    public void Init(int photonId)
    {
        PhotonNetwork.GetPhotonView(photonId).GetComponent<Shooting>().bulletsPool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public void IncreaseVelocity(float multiplayer)
    {
        rigidbody.velocity *= multiplayer;
    }

    public void Shot(Vector2 position, Vector2 direction)
    {
        rigidbody.velocity = direction.normalized * speed;
        transform.position = position;
    }

    public void Restart()
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}