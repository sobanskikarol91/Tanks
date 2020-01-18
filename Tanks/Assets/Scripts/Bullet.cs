using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public Player Owner { get; private set; }
    [SerializeField] float speed = 10;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        if (photonView.IsMine)
            Invoke("DestroyAfterTime", 3f);
        else
            Debug.Log("Its not my bullet");
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

    public void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    public void InitializeBullet(Player owner, Vector3 direction, float lag)
    {
        Owner = owner;
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }

    void DestroyAfterTime()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}