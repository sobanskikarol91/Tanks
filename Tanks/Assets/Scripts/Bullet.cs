using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public Player Owner { get; private set; }
    [SerializeField] float speed = 10;


    public void Start()
    {
        if (photonView.IsMine)
            Invoke("DestroyAfterTime", 3f);
        else
            Debug.Log("Its not my bullet");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    public void InitializeBullet(Player owner, Vector3 direction, float lag)
    {
        Owner = owner;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }
    
    void DestroyAfterTime()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}