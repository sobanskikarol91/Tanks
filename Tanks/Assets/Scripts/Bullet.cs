using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Bullet : MonoBehaviourPun, IPunInstantiateMagicCallback, IRestart
{
    public Player Owner { get; private set; }
    [SerializeField] float speed = 10;
    private new Rigidbody2D rigidbody;


    private void Awake()
    {
        SpawnManager.spawnedObjects.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetRotationAccordingToMovement();
    }

    private void SetRotationAccordingToMovement()
    {
        float angle = 360 - Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void InitializeBullet(Player owner, Vector3 direction, float lag)
    {
        Owner = owner;
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }

    public void IncreaseVelocity(float multiplayer)
    {
        rigidbody.velocity *= multiplayer;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("bullet: " + info.photonView.gameObject.name + " " + info.Sender.NickName);
        gameObject.SetActive(false);
    }

    [PunRPC]
    public void Shot(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void Restart()
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SpawnManager.spawnedObjects.Remove(this);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }
}