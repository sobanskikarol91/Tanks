using UnityEngine;
using Photon.Pun;

public class RigidbodySync : MonoBehaviourPun, IPunObservable
{
    private new Rigidbody2D rigidbody;

    private Vector2 position;
    private Quaternion rotation;
    private Vector2 velocity;
    private float angularVelocity;

    private float lerpFactor = 5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidbody.velocity);
            stream.SendNext(rigidbody.angularVelocity);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            velocity = (Vector2)stream.ReceiveNext();
            angularVelocity = (float)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
            UpdateData();
    }

    private void UpdateData()
    {
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpFactor);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerpFactor);
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }
}