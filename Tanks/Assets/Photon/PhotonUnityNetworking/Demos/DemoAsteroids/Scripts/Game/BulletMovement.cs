using UnityEngine;
using Photon.Pun;

public class BulletMovement : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D rb;
    private Vector2 networkPosition;
    private float networkRotation;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
            rb.rotation = Mathf.Lerp(rb.rotation, networkRotation, Time.fixedDeltaTime * 100);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkRotation = (float)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += rb.velocity * lag;
        }
    }
}